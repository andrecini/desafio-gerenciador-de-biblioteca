using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Command.UpdateInventoryStatus
{
    public record UpdateInventoryStatusCommand(int Id, bool Available) : IRequest<Inventory>;
}
