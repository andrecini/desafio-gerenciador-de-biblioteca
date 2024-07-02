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
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            var response = new CustomResponse<IEnumerable<Inventory>>(data, "Inventários Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            var response = new CustomResponse<Inventory>(data, "Inventário Recupedado com Sucesso!");

            return data != null ? Ok(response) : NoContent();
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(InventoryFilter filter)
        {
            var data = await _service.FindAsync(filter);
            var response = new CustomResponse<IEnumerable<Inventory>>(data, "Inventários Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(InventoryDTO request)
        {
            var data = await _service.AddAsync(request);
            var response = new CustomResponse<Inventory>(data, "Inventário Adicionado com Sucesso!");

            var locationUri = Url.Link(nameof(GetById), new { id = data.Id });

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, InventoryDTO request)
        {
            var data = await _service.Update(id, request);
            var response = new CustomResponse<Inventory>(data, "Inventários Atualizado com Sucesso!");

            return Ok(response);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var data = await _service.Remove(id);
            var response = new CustomResponse<bool>(data, "Inventários Removido com Sucesso!");

            return Ok(response);
        }
    }
}
