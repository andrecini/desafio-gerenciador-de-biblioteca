using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.UpdateInventoryStatus
{
    public record UpdateInventoryStatusCommand(int Id, bool Available) : IRequest<CustomResponse<Inventory>>;
}
