using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Domain.Services.Base;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;

namespace Desafios.GerenciadorBiblioteca.Domain.Services
{
    public interface ILoanService : IService<LoanDTO, Loan>
    {
        Task<IEnumerable<Loan>> FindAsync(LoanFilter filter);
    }
}
