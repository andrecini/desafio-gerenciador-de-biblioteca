using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBooksDetailsByLibrary
{
    public record GetBooksDetailsByLibraryQuery(int Page, int Size, int LibraryId) : IRequest<CustomResponse<IEnumerable<BookDetailsViewModel>>>;
}
