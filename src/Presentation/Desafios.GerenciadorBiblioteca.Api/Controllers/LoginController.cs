using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UpdateUserPassword;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UserLogin;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserByEmail;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserById;
using Desafios.GerenciadorBiblioteca.Service.CQRS.VerificationCodes.Commands.GenerateVerificationCode;
using Desafios.GerenciadorBiblioteca.Service.CQRS.VerificationCodes.Commands.ValidateVerificationCode;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using Desafios.GerenciadorBiblioteca.Service.Security.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController(
        ILogger<LoginController> logger,
        IMediator mediator,
        ITokenService tokenService,
        IHangfireApiManager hangfireApiManager) : ControllerBase
    {
        private readonly ILogger<LoginController> _logger = logger;
        private readonly IMediator _mediator = mediator;
        private readonly IHangfireApiManager _hangfireApiManager = hangfireApiManager;

        private readonly ITokenService _tokenService = tokenService;

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("user/confirmation")]
        public async Task<IActionResult> UserConfirmation(GetUserByEmailQuery request)
        {
            var response = await _mediator.Send(request);

            await _hangfireApiManager.GetAsync(response.Data.Id, response.Data.Email);

            return Ok(response);
        }

        [HttpPost("code/confirmation")]
        public async Task<IActionResult> CodeConfirmation(ValidateVerificationCodeCommand request)
        {
            var data = await _mediator.Send(request);

            GetUserByIdQuery query = new(request.UserId);
            var userData = await _mediator.Send(query);

            var token = _tokenService.GenerateJwtToken(userData.Data.Id, userData.Data.Name, userData.Data.Role);

            var response = new CustomResponse<TokenViewModel>(token, "Código Confirmado com sucesso!");

            return Ok(response);
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> ResetPassword(int id, UpdateUserPasswordCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(true);
        }
    }
}
