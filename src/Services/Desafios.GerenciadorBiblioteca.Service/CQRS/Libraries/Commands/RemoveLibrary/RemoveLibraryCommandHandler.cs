using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Commands.RemoveLibrary
{
    internal class RemoveLibraryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveLibraryCommand, CustomResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<bool>> Handle(RemoveLibraryCommand request, CancellationToken cancellationToken)
        {
            var libraryRegistered = await _unitOfWork.Libraries.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhuma Biblioteca foi encontrada com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _unitOfWork.Libraries.Remove(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ?
                 new CustomResponse<bool>(true, "Biblioteca removida com sucesso!") :
                 throw new CustomException("Não foi possível removida a Biblioteca. Tente novamente!", HttpStatusCode.InternalServerError);
        }
    }
}
