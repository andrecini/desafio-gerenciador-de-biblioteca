using Desafios.GerenciadorBiblioteca.Domain.Application.Entities.Loans;
using Desafios.GerenciadorBiblioteca.Domain.Application.Services.Base;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;

namespace Desafios.GerenciadorBiblioteca.Domain.Application.Services
{
    public interface ILoanService : IService<Loan>
    {
        Task<IEnumerable<Loan>> FindAsync(LoanFilter filter);
    }
}
