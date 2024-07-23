using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.RemoveInventory
{
    public class RemoveInventoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveInventoryCommand, CustomResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<bool>> Handle(RemoveInventoryCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<RemoveInventoryCommandValidator, RemoveInventoryCommand>(request);

            var InventoryRegistered = await _unitOfWork.Inventories.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Inventário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _unitOfWork.Inventories.Remove(InventoryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ?
                new CustomResponse<bool>(true, "Inventário removido com Sucesso!") :
                throw new CustomException("Não foi possível removido o Inventário. Tente novamente!", HttpStatusCode.InternalServerError);
        }
    }
}
