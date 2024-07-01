using Desafios.GerenciadorBiblioteca.Domain.Application.Services;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;
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
        public async Task<IActionResult> AddAsync(Library library)
        {
            var data = await _service.AddAsync(library);

            return Ok(data);
        }

        [HttpPut]
        public IActionResult Update(Library library)
        {
            var data = _service.Update(library);

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
