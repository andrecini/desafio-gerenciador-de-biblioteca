using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetAllLoans
{
    public class GetAllLoansQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllLoansQuery, IEnumerable<Loan>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Loan>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetAllLoansQueryValidator, GetAllLoansQuery>(request);

            var data = await _unitOfWork.Loans.GetAllAsync();
            
            var paginatedData = data.Take(request.Size).Skip(request.Page);

            return paginatedData;
        }
    }
}
