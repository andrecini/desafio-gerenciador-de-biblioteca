using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.UpdateInventory
{
    public class UpdateInventoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateInventoryCommand, Inventory>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Inventory> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<UpdateInventoryCommandValidator, UpdateInventoryCommand>(request);

            var InventoryRegistered = await _unitOfWork.Inventories.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Inventário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound); //TODO: Criar Lógica de verificação de ID

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
