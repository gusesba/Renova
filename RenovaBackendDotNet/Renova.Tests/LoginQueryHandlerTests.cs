using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MPS.Renova.Domain.Settings;
using Renova.Domain.Model;
using Renova.Persistence;
using Renova.Service.Handlers;
using Renova.Service.Queries;

namespace Renova.Tests;

public class LoginQueryHandlerTests
{
    private static RenovaDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<RenovaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new RenovaDbContext(options);
    }

    private static SettingsWebApi CreateSettings()
    {
        var configValues = new Dictionary<string, string?>
        {
            ["AuthSettings:Key"] = "supersecretkeysupersecretkey1234",
            ["AuthSettings:Issuer"] = "renova-tests",
            ["AuthSettings:Audience"] = "renova-tests"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configValues)
            .Build();

        return new SettingsWebApi(configuration);
    }

    [Fact]
    public async Task Handle_ReturnsToken_WhenCredentialsAreValid()
    {
        await using var context = CreateContext();
        var password = "Senha@123";
        var user = new UsuarioModel
        {
            Email = "cliente@renova.com",
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(password)
        };
        context.Usuario.Add(user);
        await context.SaveChangesAsync();

        var handler = new LoginQueryHandler(context, CreateSettings());
        var request = new LoginQuery
        {
            Email = user.Email,
            Senha = password
        };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.False(string.IsNullOrWhiteSpace(result.Token));
    }

    [Fact]
    public async Task Handle_ReturnsNull_WhenPasswordIsInvalid()
    {
        await using var context = CreateContext();
        var user = new UsuarioModel
        {
            Email = "cliente@renova.com",
            SenhaHash = BCrypt.Net.BCrypt.HashPassword("Senha@123")
        };
        context.Usuario.Add(user);
        await context.SaveChangesAsync();

        var handler = new LoginQueryHandler(context, CreateSettings());
        var request = new LoginQuery
        {
            Email = user.Email,
            Senha = "senha-incorreta"
        };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_ReturnsNull_WhenUserDoesNotExist()
    {
        await using var context = CreateContext();
        var handler = new LoginQueryHandler(context, CreateSettings());
        var request = new LoginQuery
        {
            Email = "inexistente@renova.com",
            Senha = "Senha@123"
        };

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.Null(result);
    }
}
