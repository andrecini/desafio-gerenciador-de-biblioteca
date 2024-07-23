using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoanById
{
    public class GetLoanByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLoanByIdQuery, CustomResponse<Loan>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<Loan>> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLoanByIdQueryValidator, GetLoanByIdQuery>(request);

            var data = await _unitOfWork.Loans.GetByIdAsync(request.Id);

            return new CustomResponse<Loan>(data, "Empréstimo recuperado com Sucesso");
        }
    }
}
