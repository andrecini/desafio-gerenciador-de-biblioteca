using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.UpdateInventory
{
    public record UpdateInventoryCommand(int Id, int LibraryId, int BookId) : IRequest<CustomResponse<Inventory>>;
}
