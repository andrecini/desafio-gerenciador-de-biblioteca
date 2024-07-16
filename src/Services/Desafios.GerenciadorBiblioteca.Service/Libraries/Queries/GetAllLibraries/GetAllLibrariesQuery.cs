using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Libraries.Queries.GetAllLibraries
{
    public record GetAllLibrariesQuery(int Page, int Size) : IRequest<IEnumerable<Library>>;
}
