using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.UpdateInventory
{
    public record UpdateInventoryCommand(int Id, int LibraryId, int BookId) : IRequest<Inventory>;
}
