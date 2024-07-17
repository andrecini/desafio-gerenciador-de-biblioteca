using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBooksDetailsByFilter
{
    public class GetBooksDetailsByFilterQueryHandler(IUnitOfWork unitOfWork, IInventoryService inventoryService) : IRequestHandler<GetBooksDetailsByFilterQuery, IEnumerable<BookDetailsViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IInventoryService _inventoryService = inventoryService;

        public async Task<IEnumerable<BookDetailsViewModel>> Handle(GetBooksDetailsByFilterQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetBooksDetailsByFilterQueryValidator, GetBooksDetailsByFilterQuery>(request); //TODO: Criar Chamada com Dapper

            var inventories = await _inventoryService.GetByLibraryAsync(request.LibraryId);

            if (request.Available != 0)
            {
                bool isAvailable = request.Available == 1;
                inventories = inventories.Where(x => x.Available == isAvailable).ToList();
            }

            var bookIds = inventories.Select(inv => inv.BookId).ToHashSet();
            var allBooks = await _unitOfWork.Books.GetAllAsync();

            if (!string.IsNullOrEmpty(request.Title))
                allBooks = allBooks.Where(x => x.Title.Contains(request.Title, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrEmpty(request.Author))
                allBooks = allBooks.Where(x => x.Author.Contains(request.Author, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrEmpty(request.ISBN))
                allBooks = allBooks.Where(x => x.ISBN.Contains(request.ISBN, StringComparison.CurrentCultureIgnoreCase));
            if (request.Year > 0)
                allBooks = allBooks.Where(x => x.Year == request.Year);

            var filteredBooks = allBooks.Where(book => bookIds.Contains(book.Id)).ToList();

            var data = filteredBooks.Select(book =>
            {
                var inventory = inventories.FirstOrDefault(inv => inv.BookId == book.Id);
                bool availableFlag = inventory != null && inventory.Available;
                return new BookDetailsViewModel(book, inventory.Id, availableFlag);
            });

            var paginatedData = data.Take(request.Size).Skip(request.Page);

            return paginatedData;
        }
    }
}