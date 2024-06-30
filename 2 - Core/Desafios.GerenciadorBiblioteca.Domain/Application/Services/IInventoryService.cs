using Desafios.GerenciadorBiblioteca.Domain.Application.Entities.Inventory;
using Desafios.GerenciadorBiblioteca.Domain.Application.Services.Base;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;

namespace Desafios.GerenciadorBiblioteca.Domain.Application.Services
{
    public interface IInventoryService : IService<Inventory>
    {
        Task<IEnumerable<Inventory>> FindAsync(InventoryFilter filter);
    }
}
