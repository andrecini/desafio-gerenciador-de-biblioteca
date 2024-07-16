using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Queries.GetInventoriesByFilter
{
    public record GetInventoriesByFilterQuery(int Page, int Size, int LibraryId, int BookId, InventoryStatus Status) : IRequest<IEnumerable<Inventory>>;
}
