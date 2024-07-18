using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetAllInventories
{
    public record GetAllInventoriesQuery(int Page, int Size) : IRequest<IEnumerable<Inventory>>;
}
