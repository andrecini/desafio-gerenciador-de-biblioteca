using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetAllLoans
{
    public class GetAllLoansQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllLoansQuery, CustomResponse<IEnumerable<Loan>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<IEnumerable<Loan>>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetAllLoansQueryValidator, GetAllLoansQuery>(request);

            var data = await _unitOfWork.Loans.GetAllAsync();

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<Loan>>(paginatedData, "Empréstimos recuperados com Sucesso!", data.Count());
        }
    }
}
