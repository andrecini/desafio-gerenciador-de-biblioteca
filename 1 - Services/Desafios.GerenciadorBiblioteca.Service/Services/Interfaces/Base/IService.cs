using Desafios.GerenciadorBiblioteca.Domain.Entities.Base;

namespace Desafios.GerenciadorBiblioteca.Service.Services.Interfaces.Base
{
    public interface IService<T, TEntity> where TEntity : IEntity<int>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(T dto);
        Task<TEntity> Update(int id, T dto);
        Task<bool> Remove(int id);
    }
}
