using Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Commands.AddBook;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Commands.RemoveBook;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Commands.UpdateBook;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetAllBooks;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBookById;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBooksByFilter;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBooksDetailsByFilter;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBooksDetailsByLibrary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController(ILogger<BooksController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<BooksController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(int page, int size)
        {
            GetAllBooksQuery request = new(page, size);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            GetBookByIdQuery request = new(id);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("filter")]
        public async Task<IActionResult> Filter(GetBooksByFilterQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("library/{libraryId}/details")]
        public async Task<IActionResult> GetDetailsByLibrary(int libraryId, int page, int size)
        {
            GetBooksDetailsByLibraryQuery request = new(page, size, libraryId);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("library/{libraryId}/details/filter")]
        public async Task<IActionResult> GetDetailsByFilter(GetBooksDetailsByFilterQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddBookCommand request)
        {
            var response = await _mediator.Send(request);

            var locationUri = Url.Link(nameof(GetById), new { id = response.Data.Id });

            return Created(locationUri, response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            RemoveBookCommand request = new(id);

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
