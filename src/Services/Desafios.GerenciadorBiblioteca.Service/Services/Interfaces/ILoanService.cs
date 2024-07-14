using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;

namespace Desafios.GerenciadorBiblioteca.Service.Services.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan> GetByIdAsync(int id);
        Task<Loan> AddAsync(LoanInputModel dto);
        Task<Loan> UpdateAsync(int id, LoanInputModel dto);
        Task<bool> RemoveAsync(int id);
        Task<IEnumerable<Loan>> GetByFilterAsync(LoanFilter filter);
        Task<IEnumerable<Loan>> GetByUserAsync(int userId);
        Task<Loan> UpdateStatusAsync(int id, bool returned);
        Task<List<LoanDetailsViewModel>> GetLoanDetailsByLibraryAsync(int libraryId);
        Task<List<LoanDetailsViewModel>> GetFilteredLoanDetailsAsync(LoanDetailsFilter filter);
    }
}
