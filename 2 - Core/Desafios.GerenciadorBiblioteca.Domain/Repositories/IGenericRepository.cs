using Desafios.GerenciadorBiblioteca.Domain.Entities;
using System.Linq.Expressions;

namespace Desafios.GerenciadorBiblioteca.Domain.Repositories
{
    public interface IGenericRepository<TEntity, T>
        where TEntity : class, IEntity<T>, new()
        where T : IComparable, IEquatable<T>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

    }
}
