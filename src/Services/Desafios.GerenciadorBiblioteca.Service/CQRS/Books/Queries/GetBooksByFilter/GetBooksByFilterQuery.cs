using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBooksByFilter
{
    public record GetBooksByFilterQuery(int Page, int Size, string Title, string Author, string ISBN, int Year) : IRequest<IEnumerable<Book>>;
}
