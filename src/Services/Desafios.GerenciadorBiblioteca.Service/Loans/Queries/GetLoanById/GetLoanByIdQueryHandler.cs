using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetLoanById
{
    public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, Loan>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLoanByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Loan> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLoanByIdQueryValidator, GetLoanByIdQuery> (request);

            var data = await _unitOfWork.Loans.GetByIdAsync(request.Id);

            return data;
        }
    }
}
