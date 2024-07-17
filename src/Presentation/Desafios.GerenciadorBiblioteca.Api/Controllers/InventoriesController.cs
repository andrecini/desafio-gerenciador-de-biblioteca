using Azure.Core;
using Desafios.GerenciadorBiblioteca.Api.Responses;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Inventories.Command.AddInventory;
using Desafios.GerenciadorBiblioteca.Service.Inventories.Command.RemoveInventory;
using Desafios.GerenciadorBiblioteca.Service.Inventories.Command.UpdateInventory;
using Desafios.GerenciadorBiblioteca.Service.Inventories.Command.UpdateInventoryStatus;
using Desafios.GerenciadorBiblioteca.Service.Inventories.Queries.GetAllInventories;
using Desafios.GerenciadorBiblioteca.Service.Inventories.Queries.GetInventoriesByFilter;
using Desafios.GerenciadorBiblioteca.Service.Inventories.Queries.GetInventoryById;
using Desafios.GerenciadorBiblioteca.Service.Inventories.Queries.GetInventoryByLibrary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoriesController(ILogger<InventoriesController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<InventoriesController> _logger = logger;
        private readonly IMediator _mediator = mediator;


        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllInventoriesQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<Inventory>>(data, "Inventários Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(GetInventoryByIdQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Inventory>(data, "Inventário Recupedado com Sucesso!");

            return data != null ? Ok(response) : NoContent();
        }

        [HttpGet("library/{libraryId}")]
        public async Task<IActionResult> GetByLibrary(int libraryId, GetInventoryByLibraryQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<Inventory>>(data, "Inventários Recupedado com Sucesso!");

            return data != null ? Ok(response) : NoContent();
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(GetInventoriesByFilterQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<Inventory>>(data, "Inventários Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddInventoryCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Inventory>(data, "Inventário Adicionado com Sucesso!");

            var locationUri = Url.Link(nameof(GetById), new { id = data.Id });

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateInventoryCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Inventory>(data, "Inventários Atualizado com Sucesso!");

            return Ok(response);
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> Update(int id, UpdateInventoryStatusCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Inventory>(data, "Inventários Atualizado com Sucesso!");

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id, RemoveInventoryCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<bool>(data, "Inventários Removido com Sucesso!");

            return Ok(response);
        }
    }
}
