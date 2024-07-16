using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetFilteredLoanDetails
{
    public record GetFilteredLoanDetailsQuery(
        int Page, 
        int Size,
        int InventoryId,
        int UserId,
        int LibraryId,
        string BookName,
        string UserName,
        DateTime LoanDate,
        DateTime LoanValidity,
        LoanStatus Status) : IRequest<IEnumerable<LoanDetailsViewModel>>;
}
