using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetAllInventories
{
    public class GetAllInventoriesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllInventoriesQuery, CustomResponse<IEnumerable<Inventory>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<IEnumerable<Inventory>>> Handle(GetAllInventoriesQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetAllInventoriesQueryValidator, GetAllInventoriesQuery>(request);

            var data = await _unitOfWork.Inventories.GetAllAsync();

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<Inventory>>(paginatedData, "Inventários recuperados com sucesso!", data.Count());
        }
    }
}
