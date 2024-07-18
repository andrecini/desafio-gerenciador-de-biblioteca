using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBooksDetailsByFilter
{
    public record GetBooksDetailsByFilterQuery(
        int Page,
        int Size,
        int LibraryId,
        string Title,
        string Author,
        string ISBN,
        int Year,
        int Available) : IRequest<IEnumerable<BookDetailsViewModel>>;
}
