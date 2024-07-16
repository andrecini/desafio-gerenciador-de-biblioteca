using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Queries.GetInventoryByLibrary
{
    public record GetInventoryByLibraryQuery(int Page, int Size, int LibraryId) : IRequest<IEnumerable<Inventory>>;
}