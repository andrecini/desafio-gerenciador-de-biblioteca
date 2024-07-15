using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Security.Interfaces;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController(ILogger<LoginController> logger, IUserService userService, ITokenService tokenService) : ControllerBase
    {
        private readonly ILogger<LoginController> _logger = logger;
        private readonly IUserService _userService = userService;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginInputModel request)
        {
            var data = await _userService.LoginAsync(request);
            var token = _tokenService.GenerateJwtToken(data.Id, data.Name, data.Role);

            var response = new UserLoginViewModel(data, token);

            return Ok(response);
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> ResetPassword(int id, string password)
        {
            var data = await _userService.UpdatePasswordAsync(id, password);
            var token = _tokenService.GenerateJwtToken(data.Id, data.Name, data.Role);

            var response = new UserLoginViewModel(data, token);

            return Ok(response);
        }
    }
}
