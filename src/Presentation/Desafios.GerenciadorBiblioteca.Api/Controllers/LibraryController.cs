using Desafios.GerenciadorBiblioteca.Api.Responses;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILogger<LibraryController> _logger;
        private readonly ILibraryService _service;

        public LibraryController(ILogger<LibraryController> logger, ILibraryService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            var response = new CustomResponse<IEnumerable<Library>>(data, "Bibliotecas Recupedadas com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            var response = new CustomResponse<Library>(data, "Biblioteca Recupedada com Sucesso!");

            return data != null ? Ok(response) : NoContent();
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter([FromBody]string name)
        {
            var data = await _service.GetByNameAsync(name);
            var response = new CustomResponse<IEnumerable<Library>>(data, "Bibliotecas Recupedadas com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(LibraryDTO request)
        {
            var data =  await _service.AddAsync(request);
            var response = new CustomResponse<Library>(data, "Biblioteca Adicionada com Sucesso!");

            var locationUri = Url.Link(nameof(GetById), new { id = data.Id });

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LibraryDTO request)
        {
            var data = await _service.UpdateAsync(id, request);
            var response = new CustomResponse<Library>(data, "Biblioteca Atualizada com Sucesso!");

            return Ok(response);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var data = await _service.RemoveAsync(id);
            var response = new CustomResponse<bool>(data, "Biblioteca Removido com Sucesso!");

            return Ok(response);
        }
    }
}
