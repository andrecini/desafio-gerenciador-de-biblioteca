using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBooksDetailsByLibrary
{
    public record GetBooksDetailsByLibraryQuery(int Page, int Size, int LibraryId) : IRequest<IEnumerable<BookDetailsViewModel>>;
}
