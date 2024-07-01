using Desafios.GerenciadorBiblioteca.Data.Context;
using Desafios.GerenciadorBiblioteca.Data.Repositories.Base;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Repositories;

namespace Desafios.GerenciadorBiblioteca.Data.Repositories
{
    public class LoanRepository(LibraryDbContext context) : GenericRepository<Loan, int>(context), ILoanRepository
    {
    }
}
