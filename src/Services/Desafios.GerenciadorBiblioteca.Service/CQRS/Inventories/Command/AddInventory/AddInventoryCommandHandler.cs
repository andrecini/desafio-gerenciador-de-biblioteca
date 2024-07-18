using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.AddInventory
{
    public class AddInventoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddInventoryCommand, Inventory>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Inventory> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<AddInventoryCommandValidator, AddInventoryCommand>(request);

            var entity = _mapper.Map<Inventory>(request);
            entity.Available = true;

            entity = await _unitOfWork.Inventories.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? entity : throw new CustomException(
                "Não foi possível adicionar o Inventário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}
