using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoryById
{
    public record GetInventoryByIdQuery(int Id) : IRequest<Inventory>;
}
