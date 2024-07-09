using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Services.Base;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Desafios.GerenciadorBiblioteca.Service.Validators;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Services
{
    public class LoanService(
        IUnitOfWork unitOfWork,
        IInventoryService inventoryService,
        IBookService bookService,
        IUserService userService,
        IMapper mapper) : BaseService, ILoanService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IInventoryService _inventoryService = inventoryService;
        private readonly IBookService _bookService = bookService;
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            var data = await _unitOfWork.Loans.GetAllAsync();

            return data;
        }

        public async Task<Loan> GetByIdAsync(int id)
        {
            CustomException.ThrowIfLessThan(0, "Id");

            var data = await _unitOfWork.Loans.GetByIdAsync(id);

            return data;
        }

        public async Task<IEnumerable<Loan>> GetByFilterAsync(LoanFilter filter)
        {
            var data = await GetAllAsync();

            data = FilterLoans(data, filter);

            return data;
        }

        public async Task<IEnumerable<Loan>> GetByUserAsync(int userId)
        {
            var data = await _unitOfWork.Loans.FindAsync(x => x.UserId == userId);

            return data;
        }

        public async Task<Loan> AddAsync(LoanDTO dto)
        {
            CustomException.ThrowIfLessThan(0, "Empréstimo");

            ValidateEntity<LoanValidator, LoanDTO>(dto);

            var entity = _mapper.Map<Loan>(dto);

            entity = await _unitOfWork.Loans.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            await _inventoryService.UpdateStatusAsync(dto.InventoryId, false);

            return result > 0 ? entity : throw new CustomException(
                "Não foi possível adicionar o Empréstimo. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<Loan> UpdateAsync(int id, LoanDTO dto)
        {
            CustomException.ThrowIfLessThan(0, "Empréstimo");

            ValidateEntity<LoanValidator, LoanDTO>(dto);

            var loanRegistered = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Emrpéstimo foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            loanRegistered.UserId = dto.UserId;
            loanRegistered.InventoryId = dto.InventoryId;
            loanRegistered.LoanDate = dto.LoanDate;
            loanRegistered.LoanValidity = dto.LoanValidity;

            _unitOfWork.Loans.Update(loanRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? loanRegistered : throw new CustomException(
                "Não foi possível atualizar o Empréstimo. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<Loan> UpdateStatusAsync(int id, bool returned)
        {
            CustomException.ThrowIfLessThan(1, "Empréstimo");

            var loanRegistered = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Empréstimo foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            loanRegistered.Returned = returned;

            _unitOfWork.Loans.Update(loanRegistered);
            var result = await _unitOfWork.SaveAsync();

            await _inventoryService.UpdateStatusAsync(loanRegistered.InventoryId, returned);

            return result > 0 ? loanRegistered : throw new CustomException(
                "Não foi possível atualizar o Empréstimo. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            CustomException.ThrowIfLessThan(0, "Id");

            var loanRegistered = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Empréstimo foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _unitOfWork.Loans.Remove(loanRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível deletar o Empréstimo. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<List<LoanDetailsDTO>> GetLoanDetailsByLibraryAsync(int libraryId)
        {
            var inventories = await inventoryService.GetByLibraryAsync(libraryId);
            var allLoans = await GetAllAsync();
            var books = await bookService.GetAllAsync();
            var users = await userService.GetAllAsync();

            var inventoryIds = inventories.Select(inv => inv.Id).ToHashSet();
            var loans = allLoans.Where(loan => inventoryIds.Contains(loan.InventoryId)).ToList();

            var bookDict = books.ToDictionary(book => book.Id, book => book.Title);
            var userDict = users.ToDictionary(user => user.Id, user => user.Name);

            var loansDtos = new List<LoanDetailsDTO>();

            foreach (var loan in loans)
            {
                var inventory = inventories.FirstOrDefault(x => x.Id == loan.InventoryId);

                if (inventory != null &&
                    bookDict.TryGetValue(inventory.BookId, out var bookTitle) &&
                    userDict.TryGetValue(loan.UserId, out var userName))
                {
                    loansDtos.Add(new LoanDetailsDTO(loan, bookTitle, userName));
                }
            }

            return loansDtos;
        }

        public async Task<List<LoanDetailsDTO>> GetFilteredLoanDetailsAsync(LoanDetailsFilter filter)
        {
            var filteredBooks = await bookService.GetByFilterAsync(new() { Title = filter.BookName });
            var filteredBooksIds = filteredBooks.Select(x => x.Id).ToHashSet();

            var inventories = await _inventoryService.GetByLibraryAsync(filter.LibraryId);
            var filteredInventories = inventories.Where(x => filteredBooksIds.Contains(x.BookId));
            var fiteredInventoriesIds =  inventories.Select(x => x.Id).ToHashSet();

            var usersFiltered = await userService.GetByNameAsync(filter.UserName);
            var usersFilteredIds = usersFiltered.Select(x => x.Id).ToHashSet();

            var bookDict = filteredBooks.ToDictionary(book => book.Id, book => book.Title);
            var userDict = usersFiltered.ToDictionary(user => user.Id, user => user.Name);

            var loansfiltered = await GetByFilterAsync(filter.LoanFilter);
            loansfiltered = loansfiltered.Where(x =>
            fiteredInventoriesIds.Contains(x.InventoryId) &&
            usersFilteredIds.Contains(x.UserId));

            var loansDtos = new List<LoanDetailsDTO>();

            foreach (var loan in loansfiltered)
            {
                var inventory = inventories.FirstOrDefault(x => x.Id == loan.InventoryId);

                if (inventory != null &&
                    bookDict.TryGetValue(inventory.BookId, out var bookTitle) &&
                    userDict.TryGetValue(loan.UserId, out var userName))
                {
                    loansDtos.Add(new LoanDetailsDTO(loan, bookTitle, userName));
                }
            }

            return loansDtos;
        }

        private IEnumerable<Loan> FilterLoans(IEnumerable<Loan> loans, LoanFilter filter)
        {
            if (filter.InventoryId > 0)
                loans = loans.Where(x => x.InventoryId == filter.InventoryId);
            if (filter.UserId > 0)
                loans = loans.Where(x => x.UserId == filter.UserId);
            if (filter.LoanDate != default)
                loans = loans.Where(x => x.LoanDate.ToShortDateString() == filter.LoanDate.ToShortDateString());
            if (filter.LoanValidity != default)
                loans = loans.Where(x => x.LoanValidity.ToShortDateString() == filter.LoanValidity.ToShortDateString());
            if (filter.Status == LoanStatus.Returned)
                loans = loans.Where(x => x.Returned == true);
            if (filter.Status == LoanStatus.Pending)
                loans = loans.Where(x => x.Returned == false);

            return loans;
        }
    }
}
