using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBooksDetailsByLibrary
{
    public class GetBooksDetailsByLibraryQueryHandler(IUnitOfWork unitOfWork, IInventoryService inventoryService) : IRequestHandler<GetBooksDetailsByLibraryQuery, IEnumerable<BookDetailsViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IInventoryService _inventoryService = inventoryService;

        public async Task<IEnumerable<BookDetailsViewModel>> Handle(GetBooksDetailsByLibraryQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetBooksDetailsByLibraryQueryValidator, GetBooksDetailsByLibraryQuery>(request);

            var inventories = await _inventoryService.GetByLibraryAsync(request.LibraryId);

            var bookIds = inventories.Select(inv => inv.BookId).ToHashSet();
            var allBooks = await _unitOfWork.Books.GetAllAsync();
            var books = allBooks.Where(book => bookIds.Contains(book.Id)).ToList();

            var data = books.Select(book =>
            {
                var inventory = inventories.FirstOrDefault(inv => inv.BookId == book.Id);
                bool available = inventory != null && inventory.Available;
                return new BookDetailsViewModel(book, inventory.Id, available);
            });

            var paginatedData = data.Take(request.Size).Take(request.Page);

            return paginatedData;
        }
    }
}
