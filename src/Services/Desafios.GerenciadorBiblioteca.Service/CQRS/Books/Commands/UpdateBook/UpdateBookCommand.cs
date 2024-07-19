using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Commands.UpdateBook
{
    public record UpdateBookCommand(int Id, string Title, string Author, string ISBN, int Year) : IRequest<CustomResponse<Book>>;
}
