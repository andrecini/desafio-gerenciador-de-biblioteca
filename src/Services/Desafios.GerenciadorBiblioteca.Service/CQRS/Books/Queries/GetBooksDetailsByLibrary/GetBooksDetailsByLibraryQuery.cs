using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBooksDetailsByLibrary
{
    public record GetBooksDetailsByLibraryQuery(int Page, int Size, int LibraryId) : IRequest<CustomResponse<IEnumerable<BookDetailsViewModel>>>;
}
