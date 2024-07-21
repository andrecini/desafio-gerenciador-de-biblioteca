using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Services;
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
            var inventories = await _unitOfWork.Inventories.FindAsync(x => x.LibraryId == request.LibraryId);
            var bookIds = inventories.Select(x => x.BookId).ToHashSet();

            var filteredBooks = books.Where(x => !bookIds.Contains(x.Id));

            Dictionary<int, string> dict = [];

            foreach (var inventory in inventories)
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
