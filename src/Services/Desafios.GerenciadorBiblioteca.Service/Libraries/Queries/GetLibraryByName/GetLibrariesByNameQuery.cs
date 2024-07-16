using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Libraries.Queries.GetLibraryByName
{
    public record GetLibrariesByNameQuery(int Page, int Size, string Name) : IRequest<IEnumerable<Library>>;
}
