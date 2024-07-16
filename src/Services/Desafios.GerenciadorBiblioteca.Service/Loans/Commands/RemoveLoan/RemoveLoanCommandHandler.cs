﻿using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Commands.RemoveLoan
{
    public class RemoveLoanCommandHandler : IRequestHandler<RemoveLoanCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveLoanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemoveLoanCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<RemoveLoanCommandValidator, RemoveLoanCommand>(request);

            CustomException.ThrowIfLessThanOne(request.Id, "Empréstimo");

            var loanRegistered = await _unitOfWork.Loans.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Empréstimo foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _unitOfWork.Loans.Remove(loanRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível deletar o Empréstimo. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}
