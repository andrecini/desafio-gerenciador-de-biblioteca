using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities.Base;

namespace Desafios.GerenciadorBiblioteca.Domain.Application.Services.Base
{
    public interface IService<T> where T : IEntity<int>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> Update(T entity);
        Task<bool> Remove(int id);
    }
}
