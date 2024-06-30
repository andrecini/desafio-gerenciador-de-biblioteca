using Desafios.GerenciadorBiblioteca.Domain.Application.Entities.Loans;
using Desafios.GerenciadorBiblioteca.Domain.Application.Enums;
using Desafios.GerenciadorBiblioteca.Domain.Application.Services;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Infra.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Services.Base;

namespace Desafios.GerenciadorBiblioteca.Service.Services
{
    public class LoanSevice(IUnitOfWork unitOfWork) : ServiceBase, ILoanService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            var data = await _unitOfWork.Loans.GetAllAsync();

            return data.Any() ? data : throw new Exception("Nenhum Emprestimo encontrado.");
        }

        public async Task<Loan> GetByIdAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(0, id);

            var data = await _unitOfWork.Loans.GetByIdAsync(id);

            return ValidateReturnedDada(data, "Nenhum Emprestimo encontrado.");
        }

        public async Task<IEnumerable<Loan>> FindAsync(LoanFilter filter)
        {
            var data = await GetAllAsync();

            data = FilterLoans(data, filter);

            return ValidateReturnedDada(data, "Nenhum Emprestimo encontrado.");
        }

        public async Task<bool> AddAsync(Loan entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _unitOfWork.Loans.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível adicionar o Emprestimo. Tente novamente!");
        }

        public async Task<bool> Update(Loan entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var libraryRegistered = await GetByIdAsync(entity.Id);

            ArgumentNullException.ThrowIfNull(libraryRegistered);

            _unitOfWork.Loans.Update(entity);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível alterar o Emprestimo. Tente novamente!");
        }

        public async Task<bool> Remove(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(0, id);

            var libraryRegistered = await GetByIdAsync(id);

            ArgumentNullException.ThrowIfNull(libraryRegistered);

            _unitOfWork.Loans.Remove(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível deletar o Emprestimo. Tente novamente!");
        }

        private IEnumerable<Loan> FilterLoans(IEnumerable<Loan> loans, LoanFilter filter)
        {
            if (filter.LibraryId >= 0)
                loans = loans.Where(x => x.LibraryId == filter.LibraryId);
            if (filter.UserId >= 0)
                loans = loans.Where(x => x.UserId == filter.UserId);
            if (filter.BookId >= 0)
                loans = loans.Where(x => x.BookId == filter.BookId);
            if (filter.LoanDate != default)
                loans = loans.Where(x => x.LoanDate == filter.LoanDate);
            if (filter.LoanValidity != default)
                loans = loans.Where(x => x.LoanValidity == filter.LoanValidity);
            if (filter.Status == LoanStatus.Returned)
                loans = loans.Where(x => x.Returned == true);
            if (filter.Status == LoanStatus.Returned)
                loans = loans.Where(x => x.Returned == false);

            return loans;
        }
    }
}
