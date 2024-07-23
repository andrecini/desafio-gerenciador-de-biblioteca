using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoriesByFilter
{
    public record GetInventoriesByFilterQuery(int Page, int Size, int LibraryId, int BookId, InventoryStatus Status) : IRequest<CustomResponse<IEnumerable<Inventory>>>;
}
