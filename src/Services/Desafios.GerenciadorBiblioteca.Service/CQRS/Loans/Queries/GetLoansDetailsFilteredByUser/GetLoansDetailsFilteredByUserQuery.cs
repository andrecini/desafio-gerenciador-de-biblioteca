using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsFilteredByUser
{
    public record GetLoansDetailsFilteredByUserQuery(
        int Page,
        int Size,
        int UserId,
        string? BookName,
        DateTime LoanDate,
        DateTime LoanValidity,
        LoanStatus Status) : IRequest<CustomResponse<IEnumerable<LoanDetailsViewModel>>>;
}
