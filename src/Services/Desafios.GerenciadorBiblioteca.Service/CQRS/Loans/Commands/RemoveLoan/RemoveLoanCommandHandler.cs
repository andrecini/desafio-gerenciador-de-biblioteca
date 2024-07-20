using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.RemoveLoan
{
    public class RemoveLoanCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveLoanCommand, CustomResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<bool>> Handle(RemoveLoanCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<RemoveLoanCommandValidator, RemoveLoanCommand>(request);

            var loanRegistered = await _unitOfWork.Loans.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Empréstimo foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound); //TODO: Criar Lógica de verificação de ID

            _unitOfWork.Loans.Remove(loanRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ?
               new CustomResponse<bool>(true, "Empréstimo removido com sucesso!") :
               throw new CustomException("Não foi possível remover o Empréstimo. Tente novamente!", HttpStatusCode.InternalServerError);
        }
    }
}
