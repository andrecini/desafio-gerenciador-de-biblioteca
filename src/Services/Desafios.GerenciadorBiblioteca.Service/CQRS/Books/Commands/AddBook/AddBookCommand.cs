using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Commands.AddBook
{
    public record AddBookCommand(string Title, string Author, string ISBN, int Year) : IRequest<CustomResponse<Book>>;
}
