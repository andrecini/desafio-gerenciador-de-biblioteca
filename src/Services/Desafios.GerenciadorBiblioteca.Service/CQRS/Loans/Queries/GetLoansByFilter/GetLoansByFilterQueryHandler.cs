using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansByFilter
{
    public class GetLoansByFilterQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLoansByFilterQuery, CustomResponse<IEnumerable<Loan>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<IEnumerable<Loan>>> Handle(GetLoansByFilterQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLoansByFilterQueryValidator, GetLoansByFilterQuery>(request);

            var data = await _unitOfWork.Loans.GetAllAsync();

            if (request.InventoryId > 0)
                data = data.Where(x => x.InventoryId == request.InventoryId);
            if (request.UserId > 0)
                data = data.Where(x => x.UserId == request.UserId);
            if (request.LoanDate != default)
                data = data.Where(x => x.LoanDate.ToShortDateString() == request.LoanDate.ToShortDateString());
            if (request.LoanValidity != default)
                data = data.Where(x => x.LoanValidity.ToShortDateString() == request.LoanValidity.ToShortDateString());
            if (request.Status == LoanStatus.Returned)
                data = data.Where(x => x.Returned == true);
            if (request.Status == LoanStatus.Pending)
                data = data.Where(x => x.Returned == false);

            var paginatedData = data.Take(request.Size).Take(request.Page);

            return new CustomResponse<IEnumerable<Loan>>(paginatedData, "Empréstimos recuperados com Sucesso!", data.Count());
        }
    }
}
