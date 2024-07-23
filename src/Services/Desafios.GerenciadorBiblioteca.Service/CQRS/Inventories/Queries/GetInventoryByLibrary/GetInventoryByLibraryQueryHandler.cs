using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoryByLibrary
{
    public class GetInventoryByLibraryQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetInventoryByLibraryQuery, CustomResponse<IEnumerable<Inventory>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<IEnumerable<Inventory>>> Handle(GetInventoryByLibraryQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetInventoryByLibraryQueryValidator, GetInventoryByLibraryQuery>(request);

            var data = await _unitOfWork.Inventories.FindAsync(x => x.LibraryId == request.LibraryId);

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<Inventory>>(paginatedData, "Inventários recuperados com sucesso!", data.Count());
        }
    }
}
