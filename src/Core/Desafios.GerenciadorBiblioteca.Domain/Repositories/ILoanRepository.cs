using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryResults;
using Desafios.GerenciadorBiblioteca.Domain.Repositories.Base;

namespace Desafios.GerenciadorBiblioteca.Domain.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan, int>
    {
        Task<IEnumerable<LoanDetailsQueryResult>> GetLoanDetailsByLibraryAsync(LoanDetailsQueryByLibraryRequest request);
        Task<IEnumerable<LoanDetailsQueryResult>> GetLoanDetailsFilteredByLibraryAsync(LoanDetailsFilteredByLibraryQueryRequest request);
        Task<IEnumerable<LoanDetailsQueryResult>> GetLoanDetailsByUserAsync(LoanDetailsQueryByUserRequest request);
        Task<IEnumerable<LoanDetailsQueryResult>> GetLoanDetailsFilteredByUserAsync(LoanDetailsFilteredByUserQueryRequest request);
    }
}
