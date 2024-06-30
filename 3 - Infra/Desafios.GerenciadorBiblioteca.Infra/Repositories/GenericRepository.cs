using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities.Base;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Repositories.Base;
using Desafios.GerenciadorBiblioteca.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Desafios.GerenciadorBiblioteca.Infra.Repositories
{
    public abstract class GenericRepository<TEntity, T> : IGenericRepository<TEntity, T>
        where TEntity : class, IEntity<T>, new() 
        where T : IComparable, IEquatable<T>
    {
        protected readonly LibraryDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        protected LibraryDbContext Context { get { return _context; } }

        public GenericRepository(LibraryDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<TEntity> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) => 
            await _dbSet.Where(predicate).ToListAsync(); 

        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

        public void Update(TEntity entity) => _dbSet.Update(entity);

        public void Remove(TEntity entity) => _dbSet.Remove(entity);
    }
}
