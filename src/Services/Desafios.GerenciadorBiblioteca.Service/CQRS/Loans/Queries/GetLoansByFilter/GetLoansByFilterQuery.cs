using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansByFilter
{
    public record GetLoansByFilterQuery(
        int Page,
        int Size,
        int InventoryId,
        int UserId,
        DateTime LoanDate,
        DateTime LoanValidity,
        LoanStatus Status) : IRequest<CustomResponse<IEnumerable<Loan>>>;
}
