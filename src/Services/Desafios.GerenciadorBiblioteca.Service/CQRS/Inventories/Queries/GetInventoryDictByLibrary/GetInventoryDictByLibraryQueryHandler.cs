using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoryDictByLibrary
{
    public class GetInventoryDictByLibraryQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetInventoryDictByLibraryQuery, CustomResponse<Dictionary<int, string>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<Dictionary<int, string>>> Handle(GetInventoryDictByLibraryQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetInventoryDictByLibraryQueryValidator, GetInventoryDictByLibraryQuery>(request);

            var books = await _unitOfWork.Books.GetAllAsync();
            var inventories = await _unitOfWork.Inventories.FindAsync(x => x.LibraryId == request.LibraryId);
            var availableInventories = inventories.Where(x => x.Available);
            var availableBookIds = availableInventories.Select(x => x.BookId).ToHashSet();

            var filteredBooks = books.Where(x => availableBookIds.Contains(x.Id));

            Dictionary<int, string> dict = [];

            foreach (var inventory in availableInventories)
            {
                var book = filteredBooks.FirstOrDefault(b => b.Id == inventory.BookId);
                if (book != null)
                {
                    dict.Add(inventory.Id, book.Title);
                }
            }

            return new CustomResponse<Dictionary<int, string>>(dict, "Dicionério de Inventários recuperados com sucesso!");
        }
    }
}
