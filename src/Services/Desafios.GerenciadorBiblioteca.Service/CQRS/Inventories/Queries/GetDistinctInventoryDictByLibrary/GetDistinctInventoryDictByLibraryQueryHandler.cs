using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetDistinctInventoryDictByLibrary
{
    public class GetDistinctInventoryDictByLibraryQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDistinctInventoryDictByLibraryQuery, CustomResponse<Dictionary<int, string>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<Dictionary<int, string>>> Handle(GetDistinctInventoryDictByLibraryQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetDistinctInventoryDictByLibraryQueryValidator, GetDistinctInventoryDictByLibraryQuery>(request);

            var books = await _unitOfWork.Books.GetAllAsync();
            var inventories = await _unitOfWork.Inventories.GetAllAsync();

            var libraryInventories = inventories.Where(x => x.LibraryId.Equals(request.LibraryId));
            var unavailableBooksIds = libraryInventories.Select(x => x.BookId).ToHashSet();

            var availableBooks = books.Where(x => !unavailableBooksIds.Contains(x.Id));

            Dictionary<int, string> dict = [];

            foreach (var book in availableBooks)
            {
                dict.Add(book.Id, book.Title);
            }

            return new CustomResponse<Dictionary<int, string>>(dict, "Dicionário de Inventários recuperados com sucesso!");
        }
    }
}
