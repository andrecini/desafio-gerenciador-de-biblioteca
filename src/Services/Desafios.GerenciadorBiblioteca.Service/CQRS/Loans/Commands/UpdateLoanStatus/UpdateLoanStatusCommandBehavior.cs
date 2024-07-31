using Desafios.GerenciadorBiblioteca.Data.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.UpdateInventoryStatus;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.UpdateLoanStatus
{
    public class UpdateLoanStatusCommandBehavior(IUnitOfWork unitOfWork, IMediator mediator) : IPipelineBehavior<UpdateLoanStatusCommand, CustomResponse<Loan>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMediator _mediator = mediator;

        public async Task<CustomResponse<Loan>> Handle(UpdateLoanStatusCommand request, RequestHandlerDelegate<CustomResponse<Loan>> next, CancellationToken cancellationToken)
        {
            var response = await next();

            var loanRegistered = await _unitOfWork.Loans.GetByIdAsync(request.Id);

            if (loanRegistered != null)
            {
                UpdateInventoryStatusCommand updateStatusRequest = new(loanRegistered.InventoryId, false);
                await _mediator.Send(updateStatusRequest);
            }

            return response;
        }
    }
}
