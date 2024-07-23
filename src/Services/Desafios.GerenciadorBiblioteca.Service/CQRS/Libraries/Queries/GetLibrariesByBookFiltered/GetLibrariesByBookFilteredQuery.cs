using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetLibrariesByBookFiltered
{
    public record GetLibrariesByBookFilteredQuery(int Page, int Size, int BookId, string? Name) : IRequest<CustomResponse<IEnumerable<Library>>>;
}
