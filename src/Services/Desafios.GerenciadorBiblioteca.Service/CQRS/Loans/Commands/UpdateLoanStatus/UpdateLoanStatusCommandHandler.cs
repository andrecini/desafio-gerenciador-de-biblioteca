using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.UpdateInventoryStatus;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.UpdateLoanStatus
{
    public class UpdateLoanStatusCommandHandler(IUnitOfWork unitOfWork, IMediator mediator) : IRequestHandler<UpdateLoanStatusCommand, CustomResponse<Loan>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMediator _mediator = mediator;

        public async Task<CustomResponse<Loan>> Handle(UpdateLoanStatusCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<UpdateLoanStatusCommandValidator, UpdateLoanStatusCommand>(request);

            var loanRegistered = await _unitOfWork.Loans.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Empréstimo foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound); //TODO: Criar Lógica de verificação de ID

            loanRegistered.Returned = request.Returned;

            _unitOfWork.Loans.Update(loanRegistered);
            var result = await _unitOfWork.SaveAsync();

            UpdateInventoryStatusCommand updateStatusRequest = new(loanRegistered.InventoryId, false);
            await _mediator.Send(updateStatusRequest);

            return result > 0 ?
               new CustomResponse<Loan>(loanRegistered, "Empréstimo alterar com sucesso!") :
               throw new CustomException("Não foi possível alterar o Empréstimo. Tente novamente!", HttpStatusCode.InternalServerError);
        }
    }
}
