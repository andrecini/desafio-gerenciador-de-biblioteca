using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UpdateUserPassword;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UserLogin;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
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
            // refatorar para mandar o token via handler
            var data = await _mediator.Send(request);

            var token = _tokenService.GenerateJwtToken(data.Data.Id, data.Data.Name, data.Data.Role);

            var response = new UserLoginViewModel(data.Data, token);

            return Ok(response);
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> ResetPassword(int id, UpdateUserPasswordCommand request)
        {
            // refatorar para mandar o token via handler

            var data = await _mediator.Send(request);

            var token = _tokenService.GenerateJwtToken(data.Data.Id, data.Data.Name, data.Data.Role);

            var response = new UserLoginViewModel(data.Data, token);

            return Ok(response);
        }
    }
}
