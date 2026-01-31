using Renova.Domain.Model;
using MediatR;

namespace Renova.Service.Queries
{
    public class RenovaQuery : IRequest<RenovaModel>
    {
        public RenovaQuery() { }

        public required int CampoQuery { get; set; }
    }
}
