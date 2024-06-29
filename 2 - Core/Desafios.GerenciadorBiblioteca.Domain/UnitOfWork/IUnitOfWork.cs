using Desafios.GerenciadorBiblioteca.Domain.Repositories;

namespace Desafios.GerenciadorBiblioteca.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBookRepository Books { get; }
        ILibraryRepository Libraries { get; }
        IUserRepository Users { get; }
        IInventoryRepository Inventories { get; }
        ILoanRepository Loans { get; }

        Task<int> SaveAsync();
    }
}
