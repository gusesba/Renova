using Renova.Domain.Model;
using MediatR;

namespace Renova.Service.Commands
{
    public class RenovaCommand : IRequest<RenovaModel>
    {
        public required string Campo2 {  get; set; }
        public int Campo3 { get; set; }
    }
}
