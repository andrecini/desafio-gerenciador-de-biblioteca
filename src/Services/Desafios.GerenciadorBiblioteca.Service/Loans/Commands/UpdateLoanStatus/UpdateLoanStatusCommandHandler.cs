using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Commands.UpdateLoanStatus
{
    public class UpdateLoanStatusCommandHandler : IRequestHandler<UpdateLoanStatusCommand, Loan>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInventoryService _inventoryService;

        public UpdateLoanStatusCommandHandler(IUnitOfWork unitOfWork, IInventoryService inventoryService)
        {
            _unitOfWork = unitOfWork;
            _inventoryService = inventoryService;
        }

        public async Task<Loan> Handle(UpdateLoanStatusCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<UpdateLoanStatusCommandValidator, UpdateLoanStatusCommand>(request);

            var loanRegistered = await _unitOfWork.Loans.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Empréstimo foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            loanRegistered.Returned = request.Returned;

            _unitOfWork.Loans.Update(loanRegistered);
            var result = await _unitOfWork.SaveAsync();

            await _inventoryService.UpdateStatusAsync(loanRegistered.InventoryId, request.Returned);

            return result > 0 ? loanRegistered : throw new CustomException(
                "Não foi possível atualizar o Empréstimo. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}
