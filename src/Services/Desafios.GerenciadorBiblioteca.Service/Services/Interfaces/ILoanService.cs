using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces.Base;

namespace Desafios.GerenciadorBiblioteca.Service.Services.Interfaces
{
    public interface ILoanService : IService<LoanDTO, Loan>
    {
        Task<IEnumerable<Loan>> GetByFilterAsync(LoanFilter filter);
        Task<IEnumerable<Loan>> GetByUserAsync(int userId);
        Task<Loan> UpdateStatusAsync(int id, bool returned);
        Task<List<LoanDetailsDTO>> GetLoanDetailsByLibraryAsync(int libraryId);
        Task<List<LoanDetailsDTO>> GetFilteredLoanDetailsAsync(LoanDetailsFilter filter);
    }
}
