using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Command.UpdateInventory
{
    public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, Inventory>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateInventoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Inventory> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            CustomException.ThrowIfNull(request, "Inventário");

            ValidatorHelper.ValidateEntity<UpdateInventoryCommandValidator, UpdateInventoryCommand>(request);

            var InventoryRegistered = await _unitOfWork.Inventories.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Inventário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            InventoryRegistered.LibraryId = request.LibraryId;
            InventoryRegistered.BookId = request.BookId;
            InventoryRegistered.Available = true;

            _unitOfWork.Inventories.Update(InventoryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? InventoryRegistered : throw new CustomException(
                "Não foi possível alterar o Inventário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}
