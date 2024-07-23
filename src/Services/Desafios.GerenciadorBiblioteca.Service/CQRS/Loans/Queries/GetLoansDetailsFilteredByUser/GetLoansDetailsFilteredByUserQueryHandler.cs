using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsFilteredByUser
{
    public class GetLoansDetailsFilteredByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetLoansDetailsFilteredByUserQuery, CustomResponse<IEnumerable<LoanDetailsViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<IEnumerable<LoanDetailsViewModel>>> Handle(GetLoansDetailsFilteredByUserQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLoansDetailsFilteredByUserQueryValidator, GetLoansDetailsFilteredByUserQuery>(request);

            var queryRequest = _mapper.Map<LoanDetailsFilteredByUserQueryRequest>(request);

            var queryResult = await _unitOfWork.Loans.GetLoanDetailsFilteredByUserAsync(queryRequest);

            var data = _mapper.Map<IEnumerable<LoanDetailsViewModel>>(queryResult);

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<LoanDetailsViewModel>>(paginatedData, "Empréstimos recuperados com Sucesso!", data.Count());
        }
    }
}

