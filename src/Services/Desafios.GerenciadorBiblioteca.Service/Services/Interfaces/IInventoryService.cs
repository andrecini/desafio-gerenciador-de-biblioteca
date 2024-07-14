using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;

namespace Desafios.GerenciadorBiblioteca.Service.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<Inventory>> GetAllAsync();
        Task<Inventory> GetByIdAsync(int id);
        Task<Inventory> AddAsync(InventoryInputModel dto);
        Task<Inventory> UpdateAsync(int id, InventoryInputModel dto);
        Task<bool> RemoveAsync(int id);
        Task<IEnumerable<Inventory>> GetByFilterAsync(InventoryFilter filter);
        Task<IEnumerable<Inventory>> GetByLibraryAsync(int libraryId);
        Task<Inventory> UpdateStatusAsync(int id, bool available);
    }
}
