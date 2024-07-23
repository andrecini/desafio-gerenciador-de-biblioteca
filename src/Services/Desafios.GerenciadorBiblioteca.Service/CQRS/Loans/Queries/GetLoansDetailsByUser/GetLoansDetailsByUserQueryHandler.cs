using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsByUser
{
    public class GetLoansDetailsByUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetLoansDetailsByUserQuery, CustomResponse<IEnumerable<LoanDetailsViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<IEnumerable<LoanDetailsViewModel>>> Handle(GetLoansDetailsByUserQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetLoansDetailsByUserQueryValidator, GetLoansDetailsByUserQuery>(request);

            var queryRequest = _mapper.Map<LoanDetailsQueryByUserRequest>(request);

            var queryResult = await _unitOfWork.Loans.GetLoanDetailsByUserAsync(queryRequest);

            var data = _mapper.Map<IEnumerable<LoanDetailsViewModel>>(queryResult);

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<LoanDetailsViewModel>>(paginatedData, "Empréstimos recuperados com Sucesso!", data.Count());

        }
    }
}
