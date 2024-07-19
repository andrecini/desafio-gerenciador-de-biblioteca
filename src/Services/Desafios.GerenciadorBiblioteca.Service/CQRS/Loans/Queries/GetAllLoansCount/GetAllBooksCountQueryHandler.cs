using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetAllLoansCount
{
    public class GetAllLoansCountQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllLoansCountQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<int> Handle(GetAllLoansCountQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Loans.GetAllAsync();
            return data.Count();
        }
    }
}
