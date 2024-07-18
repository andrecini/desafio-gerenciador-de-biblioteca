using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Commands.AddBook
{
    public record AddBookCommand(string Title, string Author, string ISBN, int Year) : IRequest<Book>;
}
