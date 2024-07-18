using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.AddInventory
{
    public record AddInventoryCommand(int LibraryId, int BookId) : IRequest<Inventory>;
}
