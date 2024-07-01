using Desafios.GerenciadorBiblioteca.Domain.Services;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
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

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var data = await _service.GetByIdAsync(id);

            return Ok(data);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterAsync([FromBody]string name)
        {
            var data = await _service.FindAsync(name);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(LibraryDTO request)
        {
            var data = await _service.AddAsync(request);

            return Ok(data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, LibraryDTO request)
        {
            var data = _service.Update(id, request);

            return Ok(data);
        }
        
        [HttpDelete]
        public IActionResult Remove(int id)
        {
            var data = _service.Remove(id);

            return Ok(data);
        }
    }
}
