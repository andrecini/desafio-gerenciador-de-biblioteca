using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Commands.UpdateBook
{
    public record UpdateBookCommand(int Id, string Title, string Author, string ISBN, int Year) : IRequest<Book>;
}
