using Desafios.GerenciadorBiblioteca.Api.Responses;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(ILogger<UserController> logger, IUserService service) : ControllerBase
    {
        private readonly ILogger<UserController> _logger = logger;
        private readonly IUserService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            var response = new CustomResponse<IEnumerable<UserViewModel>>(data, "Usuários Recupedados com Sucesso!");

            return data.Any() ? Ok(data) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            var response = new CustomResponse<UserViewModel>(data, "Usuário Recupedado com Sucesso!");

            return data != null ? Ok(data) : NoContent();
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter([FromBody]string name)
        {
            var data = await _service.GetByNameAsync(name);
            var response = new CustomResponse<IEnumerable<UserViewModel>>(data, "Usuários Recupedados com Sucesso!");

            return data.Any() ? Ok(data) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserRegisterInputModel request)
        {
            var data = await _service.AddAsync(request);
            var response = new CustomResponse<UserViewModel>(data, "Usuário Adicionado com Sucesso!");

            var locationUri = Url.Link(nameof(GetById), new { id = data.Id });

            return Created(locationUri, data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateInputModel request)
        {
            var data = await _service.UpdateAsync(id, request);
            var response = new CustomResponse<UserViewModel>(data, "Usuário Atualizado com Sucesso!");

            return Ok(response);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var data = await _service.RemoveAsync(id);
            var response = new CustomResponse<bool>(data, "Usuário Removido com Sucesso!");

            return Ok(response);
        }
    }
}
