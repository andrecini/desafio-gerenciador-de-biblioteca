using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.AddInventory;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.RemoveInventory;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.UpdateInventory;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.UpdateInventoryStatus;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetAllInventories;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetDistinctInventoryDictByLibrary;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoriesByFilter;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoryById;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoryByLibrary;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoryDictByLibrary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoriesController(ILogger<InventoriesController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<InventoriesController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(int page, int size)
        {
            GetAllInventoriesQuery request = new(page, size);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            GetInventoryByIdQuery request = new(id);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("library/{libraryId}")]
        public async Task<IActionResult> GetByLibrary(int libraryId, int page, int size)
        {
            GetInventoryByLibraryQuery request = new(page, size, libraryId);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("library/{libraryId}/books/dict")]
        public async Task<IActionResult> GetDictByLibrary(int libraryId)
        {
            GetInventoryDictByLibraryQuery request = new(libraryId);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("library/{libraryId}/books/dict/distinct")]
        public async Task<IActionResult> GetDistinctDictByLibrary(int libraryId)
        {
            GetDistinctInventoryDictByLibraryQuery request = new(libraryId);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("filter")]
        public async Task<IActionResult> GetFiltered(GetInventoriesByFilterQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddInventoryCommand request)
        {
            var response = await _mediator.Send(request);

            var locationUri = Url.Link(nameof(GetById), new { id = response.Data.Id });

            return Created(locationUri, response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateInventoryCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [Authorize]
        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateInventoryStatusCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            RemoveInventoryCommand request = new(id);

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
