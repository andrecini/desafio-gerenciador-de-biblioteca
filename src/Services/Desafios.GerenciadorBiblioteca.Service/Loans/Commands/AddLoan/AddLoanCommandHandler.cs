using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Desafios.GerenciadorBiblioteca.Service.Validators;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Commands.AddLoan
{
    public class AddLoanCommandHandler(IUnitOfWork unitOfWork, IInventoryService inventoryService, IMapper mapper) : IRequestHandler<AddLoanCommand, Loan>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IInventoryService _inventoryService = inventoryService;
        private readonly IMapper _mapper = mapper;

        public async Task<Loan> Handle(AddLoanCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<AddLoanCommandValidator, AddLoanCommand>(request);

            var entity = _mapper.Map<Loan>(request);

            entity = await _unitOfWork.Loans.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            await _inventoryService.UpdateStatusAsync(request.InventoryId, false);

            return result > 0 ? entity : throw new CustomException(
                "Não foi possível adicionar o Empréstimo. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}
