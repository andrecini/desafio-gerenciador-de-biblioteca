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
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _service.GetAllAsync();

            return data.Any() ? Ok(data) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var data = await _service.GetByIdAsync(id);

            return data != null ? Ok(data) : NoContent();
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterAsync(BookFilter request)
        {
            var data = await _service.FindAsync(request);

            return data.Any() ? Ok(data) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(BookDTO request)
        {
            await _service.AddAsync(request);

            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookDTO request)
        {
            _service.Update(id, request);

            return Ok();
        }
        
        [HttpDelete]
        public IActionResult Remove(int id)
        {
            _service.Remove(id);

            return Ok();
        }
    }
}
