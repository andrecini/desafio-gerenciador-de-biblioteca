using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoryById
{
    public record GetInventoryByIdQuery(int Id) : IRequest<CustomResponse<Inventory>>;
}
