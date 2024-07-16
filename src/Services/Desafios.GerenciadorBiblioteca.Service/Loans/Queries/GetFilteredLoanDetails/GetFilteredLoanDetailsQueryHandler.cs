using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetFilteredLoanDetails
{
    public class GetFilteredLoanDetailsQueryHandler : IRequestHandler<GetFilteredLoanDetailsQuery, IEnumerable<LoanDetailsViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFilteredLoanDetailsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LoanDetailsViewModel>> Handle(GetFilteredLoanDetailsQuery request, CancellationToken cancellationToken)
        {
            var filteredBooks = await _unitOfWork.Books.FindAsync(x => x.Title.Contains(request.BookName, StringComparison.CurrentCultureIgnoreCase));
            var filteredBooksIds = filteredBooks.Select(x => x.Id).ToHashSet();

            var inventories = await _unitOfWork.Inventories.FindAsync(x => x.LibraryId == request.LibraryId);
            var filteredInventories = inventories.Where(x => filteredBooksIds.Contains(x.BookId));
            var filteredInventoriesIds = inventories.Select(x => x.Id).ToHashSet();

            var usersFiltered = await _unitOfWork.Users.FindAsync(x => x.Name.Contains(request.UserName, StringComparison.CurrentCultureIgnoreCase));
            var usersFilteredIds = usersFiltered.Select(x => x.Id).ToHashSet();

            var bookDict = filteredBooks.ToDictionary(book => book.Id, book => book.Title);
            var userDict = usersFiltered.ToDictionary(user => user.Id, user => user.Name);

            var loans = await _unitOfWork.Loans.GetAllAsync();
            var loansFiltered = FilterLoans(loans, request);
            loansFiltered = loansFiltered.Where(x => filteredInventoriesIds.Contains(x.InventoryId) && usersFilteredIds.Contains(x.UserId));

            var loansDtos = new List<LoanDetailsViewModel>();

            foreach (var loan in loansFiltered)
            {
                var inventory = inventories.FirstOrDefault(x => x.Id == loan.InventoryId);

                if (inventory != null &&
                    bookDict.TryGetValue(inventory.BookId, out var bookTitle) &&
                    userDict.TryGetValue(loan.UserId, out var userName))
                {
                    loansDtos.Add(new LoanDetailsViewModel(loan, bookTitle, userName));
                }
            }

            var paginatedLoansViewModels = loansDtos.Take(request.Size).Skip(request.Page);

            return paginatedLoansViewModels;
        }

        private IEnumerable<Loan> FilterLoans(IEnumerable<Loan> loans, GetFilteredLoanDetailsQuery request)
        {
            if (request.InventoryId > 0)
                loans = loans.Where(x => x.InventoryId == request.InventoryId);
            if (request.UserId > 0)
                loans = loans.Where(x => x.UserId == request.UserId);
            if (request.LoanDate != default)
                loans = loans.Where(x => x.LoanDate.ToShortDateString() == request.LoanDate.ToShortDateString());
            if (request.LoanValidity != default)
                loans = loans.Where(x => x.LoanValidity.ToShortDateString() == request.LoanValidity.ToShortDateString());
            if (request.Status == LoanStatus.Returned)
                loans = loans.Where(x => x.Returned == true);
            if (request.Status == LoanStatus.Pending)
                loans = loans.Where(x => x.Returned == false);

            return loans;
        }
    }
}
    
