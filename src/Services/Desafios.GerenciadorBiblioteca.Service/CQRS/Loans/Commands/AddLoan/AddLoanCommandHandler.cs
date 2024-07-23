using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.UpdateInventoryStatus;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.AddLoan
{
    public class AddLoanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator) : IRequestHandler<AddLoanCommand, CustomResponse<Loan>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IMediator _mediator = mediator;

        public async Task<CustomResponse<Loan>> Handle(AddLoanCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<AddLoanCommandValidator, AddLoanCommand>(request);

            var entity = _mapper.Map<Loan>(request);

            entity = await _unitOfWork.Loans.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            UpdateInventoryStatusCommand updateStatusRequest = new(request.InventoryId, false);
            await _mediator.Send(updateStatusRequest);

            return result > 0 ?
               new CustomResponse<Loan>(entity, "Empréstimo adicionado com sucesso!") :
               throw new CustomException("Não foi possível adicionar o Empréstimo. Tente novamente!", HttpStatusCode.InternalServerError);
        }
    }
}
