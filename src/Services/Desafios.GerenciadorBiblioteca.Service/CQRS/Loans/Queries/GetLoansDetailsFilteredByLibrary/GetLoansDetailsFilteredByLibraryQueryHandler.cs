using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsFilteredByLibrary
{
    public class GetLoansDetailsFilteredByLibraryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetLoansDetailsFilteredByLibraryQuery, CustomResponse<IEnumerable<LoanDetailsViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<IEnumerable<LoanDetailsViewModel>>> Handle(GetLoansDetailsFilteredByLibraryQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLoansDetailsFilteredByLibraryQueryValidator, GetLoansDetailsFilteredByLibraryQuery>(request);

            var queryRequest = _mapper.Map<LoanDetailsFilteredByLibraryQueryRequest>(request);

            var queryResult = await _unitOfWork.Loans.GetLoanDetailsFilteredByLibraryAsync(queryRequest);

            var data = _mapper.Map<IEnumerable<LoanDetailsViewModel>>(queryResult);

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<LoanDetailsViewModel>>(paginatedData, "Empréstimos recuperados com Sucesso!", data.Count());
        }
    }
}

