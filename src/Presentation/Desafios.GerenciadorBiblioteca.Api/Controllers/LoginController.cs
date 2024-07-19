using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UpdateUserPassword;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UserLogin;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Security.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController(ILogger<LoginController> logger, IMediator mediator, ITokenService tokenService) : ControllerBase
    {
        private readonly ILogger<LoginController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        private readonly ITokenService _tokenService = tokenService;

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginCommand request)
        {
            var data = await _mediator.Send(request);

            var token = _tokenService.GenerateJwtToken(data.Id, data.Name, data.Role);

            var response = new UserLoginViewModel(data, token);

            return Ok(response);
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> ResetPassword(int id, UpdateUserPasswordCommand request)
        {
            var data = await _mediator.Send(request);

            var token = _tokenService.GenerateJwtToken(data.Id, data.Name, data.Role);

            var response = new UserLoginViewModel(data, token);

            return Ok(response);
        }
    }
}
