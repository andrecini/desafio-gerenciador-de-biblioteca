using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Libraries.Queries.GetLibraryById
{
    public record GetLibraryByIdQuery(int Id) : IRequest<Library>;
}
