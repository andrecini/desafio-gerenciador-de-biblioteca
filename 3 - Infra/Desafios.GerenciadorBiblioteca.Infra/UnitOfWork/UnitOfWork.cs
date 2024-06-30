using Desafios.GerenciadorBiblioteca.Domain.Infra.Repositories;
using Desafios.GerenciadorBiblioteca.Domain.Infra.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Infra.Context;
using Desafios.GerenciadorBiblioteca.Infra.Repositories;

namespace Desafios.GerenciadorBiblioteca.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ILibraryRepository Libraries { get; private set; }
        public IBookRepository Books { get; private set; }
        public IUserRepository Users { get; private set; }
        public IInventoryRepository Inventories { get; private set; }
        public ILoanRepository Loans { get; private set; }

        private readonly LibraryDbContext _context;

        public UnitOfWork(LibraryDbContext context)
        {
            _context = context;

            Books = new BookRepository(_context);
            Libraries = new LibraryRepository(_context);
            Users = new UserRepository(_context);
            Inventories = new InventoryRepository(_context);
            Loans = new LoanRepository(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
