using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetLibrariesByBook
{
    public record GetLibrariesByBookQuery(int Page, int Size, int BookId) : IRequest<CustomResponse<IEnumerable<Library>>>;
}
