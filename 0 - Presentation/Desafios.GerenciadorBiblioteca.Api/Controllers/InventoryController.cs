using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryService _service;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService service)
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
        public async Task<IActionResult> FilterAsync(InventoryFilter filter)
        {
            var data = await _service.FindAsync(filter);

            return data.Any() ? Ok(data) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(InventoryDTO request)
        {
            await _service.AddAsync(request);

            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, InventoryDTO request)
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
