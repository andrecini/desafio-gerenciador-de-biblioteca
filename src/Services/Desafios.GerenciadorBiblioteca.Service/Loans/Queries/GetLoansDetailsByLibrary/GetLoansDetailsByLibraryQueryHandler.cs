using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetLoansDetailsByLibrary
{
    public class GetLoansDetailsByLibraryQueryHandler : IRequestHandler<GetLoansDetailsByLibraryQuery, IEnumerable<LoanDetailsViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLoansDetailsByLibraryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;;
        }

        public async Task<IEnumerable<LoanDetailsViewModel>> Handle(GetLoansDetailsByLibraryQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLoansDetailsByLibraryQueryValidator, GetLoansDetailsByLibraryQuery>(request);

            var inventories = await _unitOfWork.Inventories.FindAsync(x => x.LibraryId == request.LibraryId);
            var allLoans = await _unitOfWork.Loans.GetAllAsync();
            var books = await _unitOfWork.Books.GetAllAsync();
            var users = await _unitOfWork.Users.GetAllAsync();

            var inventoryIds = inventories.Select(inv => inv.Id).ToHashSet();
            var loans = allLoans.Where(loan => inventoryIds.Contains(loan.InventoryId)).ToList();

            var bookDict = books.ToDictionary(book => book.Id, book => book.Title);
            var userDict = users.ToDictionary(user => user.Id, user => user.Name);

            var loansDtos = new List<LoanDetailsViewModel>();

            foreach (var loan in loans)
            {
                var inventory = inventories.FirstOrDefault(x => x.Id == loan.InventoryId);

                if (inventory != null &&
                    bookDict.TryGetValue(inventory.BookId, out var bookTitle) &&
                    userDict.TryGetValue(loan.UserId, out var userName))
                {
                    loansDtos.Add(new LoanDetailsViewModel(loan, bookTitle, userName));
                }
            }

            return loansDtos;
        }
    }
}
