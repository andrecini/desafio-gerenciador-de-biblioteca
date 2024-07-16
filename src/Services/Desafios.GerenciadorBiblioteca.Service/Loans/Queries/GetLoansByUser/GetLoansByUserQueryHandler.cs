using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetLoansByUser
{
    public class GetLoansByUserQueryHandler : IRequestHandler<GetLoansByUserQuery, IEnumerable<Loan>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLoansByUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Loan>> Handle(GetLoansByUserQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLoansByUserQueryValidator, GetLoansByUserQuery>(request);

            var data = await _unitOfWork.Loans.FindAsync(x => x.UserId == request.UserId);

            var paginatedData = data.Take(request.Size).Skip(request.Page);

            return paginatedData;
        }
    }
}
