using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetOverdueLoansIds
{
    public record GetOverdueLoansIdsQuery() : IRequest<CustomResponse<IEnumerable<OverdueLoansDetailsViewModel>>>;
}
