using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Command.RemoveInventory
{
    public record RemoveInventoryCommand(int Id) : IRequest<bool>;
}
