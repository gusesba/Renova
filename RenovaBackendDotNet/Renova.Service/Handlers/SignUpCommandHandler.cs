using Renova.Domain.Model;
using Renova.Domain.Model.Dto;
using Renova.Persistence;
using Renova.Service.Commands;
using Renova.Service.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Renova.Service.Handlers
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, LoginDto>
    {
        private readonly RenovaDbContext _context;
        private readonly IMediator _mediator;
        public SignUpCommandHandler(RenovaDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<LoginDto> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var usuarioExistente = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (usuarioExistente != null)
                throw new Exception("Email já cadastrado.");

            string senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

            var novoUsuario = new UsuarioModel()
            {
                Email = request.Email,
                SenhaHash = senhaHash,
            };

            await _context.Usuario.AddAsync(novoUsuario);
            await _context.SaveChangesAsync();

            return await _mediator.Send(new LoginQuery()
            {
                Email = request.Email,
                Senha = request.Senha
            });
        }
    }
}
