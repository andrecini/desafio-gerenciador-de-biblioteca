using Desafios.GerenciadorBiblioteca.Api.Responses;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.Libraries.Commands.AddLibrary;
using Desafios.GerenciadorBiblioteca.Service.Libraries.Commands.RemoveLibrary;
using Desafios.GerenciadorBiblioteca.Service.Libraries.Commands.UpdateLibrary;
using Desafios.GerenciadorBiblioteca.Service.Libraries.Queries.GetAllLibraries;
using Desafios.GerenciadorBiblioteca.Service.Libraries.Queries.GetLibraryById;
using Desafios.GerenciadorBiblioteca.Service.Libraries.Queries.GetLibraryByName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrariesController(ILogger<LibrariesController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<LibrariesController> _logger = logger;
        private readonly IMediator _mediator = mediator;


        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllLibrariesQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<Library>>(data, "Bibliotecas Recupedadas com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(GetLibraryByIdQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Library>(data, "Biblioteca Recupedada com Sucesso!");

            return data != null ? Ok(response) : NoContent();
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(GetLibrariesByNameQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<Library>>(data, "Bibliotecas Recupedadas com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLibraryCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Library>(data, "Biblioteca Adicionada com Sucesso!");

            var locationUri = Url.Link(nameof(GetById), new { id = data.Id });

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateLibraryCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Library>(data, "Biblioteca Atualizada com Sucesso!");

            return Ok(response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id, RemoveLibraryCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<bool>(data, "Biblioteca Removido com Sucesso!");

            return Ok(response);
        }
    }
}
