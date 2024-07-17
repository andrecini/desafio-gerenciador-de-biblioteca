using Desafios.GerenciadorBiblioteca.Api.Responses;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.Books.Commands.AddBook;
using Desafios.GerenciadorBiblioteca.Service.Books.Commands.RemoveBook;
using Desafios.GerenciadorBiblioteca.Service.Books.Commands.UpdateBook;
using Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetAllBooks;
using Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBookById;
using Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBooksByFilter;
using Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBooksDetailsByFilter;
using Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBooksDetailsByLibrary;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController(ILogger<BooksController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<BooksController> _logger = logger;
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllBooksQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<Book>>(data, "Livros Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, GetBookByIdQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Book>(data, "Livro Recupedado com Sucesso!");

            return data != null ? Ok(response) : NoContent();
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(GetBooksByFilterQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<Book>>(data, "Livros Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpPost("filter/details")]
        public async Task<IActionResult> GetDetailsByFilter(GetBooksDetailsByFilterQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<BookDetailsViewModel>>(data, "Livros Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpPost("library/{libraryId}")]
        public async Task<IActionResult> GetDetailsByLibrary(int libraryId, GetBooksDetailsByLibraryQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<BookDetailsViewModel>>(data, "Livros Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Book>(data, "Livro Adicionado com Sucesso!");

            var locationUri = Url.Link(nameof(GetById), new { id = data.Id });

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Book>(data, "Livro Atualizado com Sucesso!");

            return Ok(response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id, RemoveBookCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<bool>(data, "Livro Removido com Sucesso!");

            return Ok(response);
        }
    }
}
