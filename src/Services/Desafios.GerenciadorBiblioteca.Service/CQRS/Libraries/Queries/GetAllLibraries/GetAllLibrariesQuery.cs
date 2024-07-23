using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetAllLibraries
{
    public record GetAllLibrariesQuery(int Page, int Size) : IRequest<CustomResponse<IEnumerable<Library>>>;
}
