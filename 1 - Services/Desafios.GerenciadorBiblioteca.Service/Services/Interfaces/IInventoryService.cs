using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Domain.Services.Base;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;

namespace Desafios.GerenciadorBiblioteca.Domain.Services
{
    public interface IInventoryService : IService<InventoryDTO, Inventory>
    {
        Task<IEnumerable<Inventory>> FindAsync(InventoryFilter filter);
    }
}
