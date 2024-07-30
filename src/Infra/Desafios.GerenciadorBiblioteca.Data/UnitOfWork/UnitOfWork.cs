using Desafios.GerenciadorBiblioteca.Data.Context;
using Desafios.GerenciadorBiblioteca.Data.Repositories;
using Desafios.GerenciadorBiblioteca.Domain.Repositories;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace Desafios.GerenciadorBiblioteca.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ILibraryRepository Libraries { get; private set; }
        public IBookRepository Books { get; private set; }
        public IUserRepository Users { get; private set; }
        public IInventoryRepository Inventories { get; private set; }
        public ILoanRepository Loans { get; private set; }
        public IVerificationCodeRepository VerificationCodes { get; private set; }

        private readonly LibraryDbContext _context;

        public UnitOfWork(LibraryDbContext context, IConfiguration configuration)
        {
            _context = context;

            Books = new BookRepository(_context, configuration);
            Libraries = new LibraryRepository(_context, configuration);
            Users = new UserRepository(_context);
            Inventories = new InventoryRepository(_context);
            Loans = new LoanRepository(_context, configuration);
            VerificationCodes = new VerificationCodeRepository(_context, configuration);
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
