using Renova.Domain.Model;
using Renova.Persistence;
using Renova.Service.Queries;
using MediatR;

namespace MPS.Renova.Service.Handlers.Assinatura
{
    public class RenovaQueryHandler : IRequestHandler<RenovaQuery, RenovaModel>
    {
        private readonly RenovaDbContext _renovaDbContext;
        public RenovaQueryHandler(RenovaDbContext renovaDbContext)
        {
            _renovaDbContext = renovaDbContext;
        }

        public async Task<RenovaModel> Handle(RenovaQuery request, CancellationToken cancellationToken)
        {
            var renova = await _renovaDbContext.Renova.FindAsync(request.CampoQuery, cancellationToken);

            return renova;
        }
    }
}
