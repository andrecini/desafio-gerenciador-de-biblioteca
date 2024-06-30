using Desafios.GerenciadorBiblioteca.Domain.Infra.Repositories;

namespace Desafios.GerenciadorBiblioteca.Domain.Infra.UnitOfWork
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
