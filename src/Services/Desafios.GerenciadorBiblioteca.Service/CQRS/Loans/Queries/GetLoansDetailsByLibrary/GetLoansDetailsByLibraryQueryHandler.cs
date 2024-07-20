using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsByLibrary
{
    public class GetLoansDetailsByLibraryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetLoansDetailsByLibraryQuery, CustomResponse<IEnumerable<LoanDetailsViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<IEnumerable<LoanDetailsViewModel>>> Handle(GetLoansDetailsByLibraryQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLoansDetailsByLibraryQueryValidator, GetLoansDetailsByLibraryQuery>(request);

            var queryRequest = _mapper.Map<LoanDetailsQueryRequest>(request);

            var queryResult = await _unitOfWork.Loans.GetLoanDetailsByLibraryAsync(queryRequest);

            var data = _mapper.Map<IEnumerable<LoanDetailsViewModel>>(queryResult);

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<LoanDetailsViewModel>>(paginatedData, "Empréstimos recuperados com Sucesso!", data.Count());

        }
    }
}
