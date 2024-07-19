using Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Commands.AddLibrary;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Commands.RemoveLibrary;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Commands.UpdateLibrary;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetAllLibraries;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetLibrariesByBook;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetLibrariesByBookFiltered;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetLibraryById;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetLibraryByName;
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
        public async Task<IActionResult> GetAll(int page, int size)
        {
            GetAllLibrariesQuery request = new(page, size);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetLibrariesByBook(int bookId, int page, int size)
        {
            var request = new GetLibrariesByBookQuery(page, size, bookId);
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("book/{id}/filtered")]
        public async Task<IActionResult> PostLibrariesByBookFiltered(int bookId, GetLibrariesByBookFilteredQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(GetLibraryByIdQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(GetLibrariesByNameQuery request)
        {
            var response = await _mediator.Send(request);
            
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLibraryCommand request)
        {
            var response = await _mediator.Send(request);

            var locationUri = Url.Link(nameof(GetById), new { id = response.Data.Id });

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateLibraryCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id, RemoveLibraryCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
