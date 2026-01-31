using Renova.Domain.Model;
using Renova.Persistence;
using Renova.Service.Commands;
using MediatR;

namespace Renova.Service.Handlers
{
    public class RenovaCommandHandler : IRequestHandler<RenovaCommand, RenovaModel>
    {
        private readonly RenovaDbContext _renovaDbContext;

        public RenovaCommandHandler(RenovaDbContext renovaDbContext)
        {
            _renovaDbContext = renovaDbContext;
        }

        public async Task<RenovaModel> Handle(RenovaCommand command, CancellationToken cancellationToken)
        {
            RenovaModel renovaModel = new()
            {
                Campo2 = command.Campo2,
                Campo3 = command.Campo3 
            };
            var result = await _renovaDbContext.Renova.AddAsync(renovaModel, cancellationToken);
            await _renovaDbContext.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }
    }
}
