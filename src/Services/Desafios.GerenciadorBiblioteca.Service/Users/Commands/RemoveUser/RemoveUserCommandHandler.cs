using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Users.Commands.RemoveUser
{
    public class RemoveUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<RemoveUserCommandValidator, RemoveUserCommand>(request);

            var userRegistered = await _unitOfWork.Users.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Usuário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _unitOfWork.Users.Remove(userRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível deletar o Usuário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}