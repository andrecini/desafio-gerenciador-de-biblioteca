using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetAllBooks
{
    public record GetAllBooksQuery(int Page, int Size) : IRequest<CustomResponse<IEnumerable<Book>>>;
}
