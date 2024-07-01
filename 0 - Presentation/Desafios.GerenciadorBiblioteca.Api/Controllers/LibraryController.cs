using Desafios.GerenciadorBiblioteca.Api.Models.Responses;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
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
        public async Task<IActionResult> FilterAsync([FromBody]string name)
        {
            var data = await _service.FindAsync(name);

            return data.Any() ? Ok(data) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(LibraryInpuDTO request)
        {
            await _service.AddAsync(request);

            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, LibraryInpuDTO request)
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
