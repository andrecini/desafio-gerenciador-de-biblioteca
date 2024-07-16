using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Commands.UpdateLoan
{
    public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, Loan>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLoanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Loan> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<UpdateLoanCommandValidator, UpdateLoanCommand>(request);

            var loanRegistered = await _unitOfWork.Loans.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Empréstimo foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            loanRegistered.UserId = request.UserId;
            loanRegistered.InventoryId = request.InventoryId;
            loanRegistered.LoanDate = request.LoanDate;
            loanRegistered.LoanValidity = request.LoanValidity;

            _unitOfWork.Loans.Update(loanRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? loanRegistered : throw new CustomException(
                "Não foi possível atualizar o Empréstimo. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}
