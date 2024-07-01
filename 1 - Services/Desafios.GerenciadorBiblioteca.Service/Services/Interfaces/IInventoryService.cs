using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces.Base;

namespace Desafios.GerenciadorBiblioteca.Service.Services.Interfaces
{
    public interface IInventoryService : IService<InventoryInputDTO, Inventory>
    {
        Task<IEnumerable<Inventory>> FindAsync(InventoryFilter filter);
    }
}
