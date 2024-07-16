using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBooksDetailsByFilter;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Inventories.Queries.GetAllInventories;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Queries.GetInventoryById
{
    public class GetInventoryByIdQueryHandler : IRequestHandler<GetInventoryByIdQuery, Inventory>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInventoryByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Inventory> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetInventoryByIdQueryValidator, GetInventoryByIdQuery>(request);

            var data = await _unitOfWork.Inventories.GetByIdAsync(id);

            return data;
        }
    }
}
