using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Libraries.Commands.RemoveLibrary
{
    internal class RemoveLibraryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveLibraryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> Handle(RemoveLibraryCommand request, CancellationToken cancellationToken)
        {
            var libraryRegistered = await _unitOfWork.Libraries.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhuma Biblioteca foi encontrada com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _unitOfWork.Libraries.Remove(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível deletar a Biblioteca. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}
