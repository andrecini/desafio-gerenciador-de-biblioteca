using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Command.RemoveInventory
{
    public class RemoveInventoryCommandHandler : IRequestHandler<RemoveInventoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveInventoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemoveInventoryCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<RemoveInventoryCommandValidator,  RemoveInventoryCommand>(request);

            var InventoryRegistered = await _unitOfWork.Inventories.GetByIdAsync(id) ??
                throw new CustomException("Nenhum Inventário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _unitOfWork.Inventories.Remove(InventoryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível deletar o Inventário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}
