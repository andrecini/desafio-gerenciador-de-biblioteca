using Desafios.GerenciadorBiblioteca.Api.Responses;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _service;

        public BookController(ILogger<BookController> logger, IBookService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            var response = new CustomResponse<IEnumerable<Book>>(data, "Livros Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            var response = new CustomResponse<Book>(data, "Livro Recupedado com Sucesso!");

            return data != null ? Ok(response) : NoContent();
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(BookFilter request)
        {
            var data = await _service.FindAsync(request);
            var response = new CustomResponse<IEnumerable<Book>>(data, "Livros Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookDTO request)
        {
            var data = await _service.AddAsync(request);
            var response = new CustomResponse<Book>(data, "Livro Adicionado com Sucesso!");

            var locationUri = Url.Link(nameof(GetById), new { id = data.Id });

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookDTO request)
        {
            var data = await _service.Update(id, request);
            var response = new CustomResponse<Book>(data, "Livro Atualizado com Sucesso!");

            return Ok(response);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var data = await _service.Remove(id);
            var response = new CustomResponse<bool>(data, "Livro Removido com Sucesso!");

            return Ok(response);
        }
    }
}
