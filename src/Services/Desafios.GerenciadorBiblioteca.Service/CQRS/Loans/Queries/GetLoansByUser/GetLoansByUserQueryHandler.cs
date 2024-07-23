using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansByUser
{
    public class GetLoansByUserQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLoansByUserQuery, CustomResponse<IEnumerable<Loan>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<IEnumerable<Loan>>> Handle(GetLoansByUserQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLoansByUserQueryValidator, GetLoansByUserQuery>(request);

            var data = await _unitOfWork.Loans.FindAsync(x => x.UserId == request.UserId);

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<Loan>>(paginatedData, "Empréstimos recuperados com Sucesso!", data.Count());
        }
    }
}
