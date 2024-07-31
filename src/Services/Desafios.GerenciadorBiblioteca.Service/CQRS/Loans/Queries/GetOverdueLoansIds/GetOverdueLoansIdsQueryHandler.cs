using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetOverdueLoansIds
{
    public class GetOverdueLoansIdsHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetOverdueLoansIdsQuery, CustomResponse<IEnumerable<OverdueLoansDetailsViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<IEnumerable<OverdueLoansDetailsViewModel>>> Handle(GetOverdueLoansIdsQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _unitOfWork.Loans.GetOverdueLoanDetailsAsync(DateTime.Now);

            var data = _mapper.Map<IEnumerable<OverdueLoansDetailsViewModel>>(queryResult);

            return new CustomResponse<IEnumerable<OverdueLoansDetailsViewModel>>(data, "Ids dos Empréstimos atrasados recuperados com Sucesso!", data.Count());
        }
    }
}
