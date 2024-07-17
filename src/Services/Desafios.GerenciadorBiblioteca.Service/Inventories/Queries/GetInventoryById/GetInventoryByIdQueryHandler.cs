using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Queries.GetInventoryById
{
    public class GetInventoryByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetInventoryByIdQuery, Inventory>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Inventory> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetInventoryByIdQueryValidator, GetInventoryByIdQuery>(request);

            var data = await _unitOfWork.Inventories.GetByIdAsync(request.Id);

            return data;
        }
    }
}
