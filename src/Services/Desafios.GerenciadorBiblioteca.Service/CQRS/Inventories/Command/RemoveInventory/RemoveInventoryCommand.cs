using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.RemoveInventory
{
    public record RemoveInventoryCommand(int Id) : IRequest<bool>;
}
