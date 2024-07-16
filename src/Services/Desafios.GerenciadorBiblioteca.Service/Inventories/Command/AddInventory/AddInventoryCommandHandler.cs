using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Validators;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Command.AddInventory
{
    public class AddInventoryCommandHandler : IRequestHandler<AddInventoryCommandHandler, IEnumerable<Book>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddInventoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Inventory> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
        {
            CustomException.ThrowIfNull(request, "Inventário");

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
