using Renova.Service.Commands;
using Renova.Service.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Renova.API.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private const string AuthCookieName = "renova_auth";
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Login([FromBody] LoginQuery request)
        {
            var token = await _mediator.Send(request);
            if (token == null)
                return Unauthorized();

            DefinirCookieAutenticacao(token.Token);

            return NoContent();
        }

        [HttpPost("registrar")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> Registrar([FromBody] SignUpCommand command)
        {
            var token = await _mediator.Send(command);

            DefinirCookieAutenticacao(token.Token);

            return NoContent();
        }

        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Logout()
        {
            RemoverCookieAutenticacao();

            return NoContent();
        }

        private void DefinirCookieAutenticacao(string token)
        {
            Response.Cookies.Append(AuthCookieName, token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });
        }

        private void RemoverCookieAutenticacao()
        {
            Response.Cookies.Delete(AuthCookieName, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
        }
    }
}
