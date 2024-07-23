using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsByUser
{
    public record GetLoansDetailsByUserQuery(int Page, int Size, int UserId) : IRequest<CustomResponse<IEnumerable<LoanDetailsViewModel>>>;
}
