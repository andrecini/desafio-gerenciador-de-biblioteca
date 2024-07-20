using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetAllInventories
{
    public record GetAllInventoriesQuery(int Page, int Size) : IRequest<CustomResponse<IEnumerable<Inventory>>>;
}
