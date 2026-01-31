using Renova.Domain.Model.Dto;
using MediatR;

namespace Renova.Service.Queries
{
    public class LoginQuery : IRequest<LoginDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
