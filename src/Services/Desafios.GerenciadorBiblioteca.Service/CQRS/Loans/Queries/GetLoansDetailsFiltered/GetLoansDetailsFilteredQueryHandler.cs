using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsFiltered
{
    public class GetLoansDetailsFilteredQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetLoansDetailsFilteredQuery, CustomResponse<IEnumerable<LoanDetailsViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<IEnumerable<LoanDetailsViewModel>>> Handle(GetLoansDetailsFilteredQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLoansDetailsFilteredQueryValidator, GetLoansDetailsFilteredQuery>(request);

            var queryRequest = _mapper.Map<LoanDetailsFilteredQueryRequest>(request);

            var queryResult = await _unitOfWork.Loans.GetLoanDetailsByFilterAsync(queryRequest);

            var data = _mapper.Map<IEnumerable<LoanDetailsViewModel>>(queryResult);

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<LoanDetailsViewModel>>(paginatedData, "Empréstimos recuperados com Sucesso!", data.Count());
        }
    }
}

