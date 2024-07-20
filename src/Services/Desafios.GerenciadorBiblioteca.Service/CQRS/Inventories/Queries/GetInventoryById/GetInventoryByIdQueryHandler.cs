using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoryById
{
    public class GetInventoryByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetInventoryByIdQuery, CustomResponse<Inventory>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<Inventory>> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetInventoryByIdQueryValidator, GetInventoryByIdQuery>(request);

            var data = await _unitOfWork.Inventories.GetByIdAsync(request.Id);

            return new CustomResponse<Inventory>(data, "Inventário recuperado com Sucesso!");
        }
    }
}
