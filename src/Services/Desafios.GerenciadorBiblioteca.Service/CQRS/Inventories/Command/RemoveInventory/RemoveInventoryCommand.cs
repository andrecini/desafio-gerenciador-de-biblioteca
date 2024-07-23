using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.RemoveInventory
{
    public record RemoveInventoryCommand(int Id) : IRequest<CustomResponse<bool>>;
}
