using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetAllBooks
{
    public record GetAllBooksQuery(int Page, int Size) : IRequest<IEnumerable<Book>>;
}
