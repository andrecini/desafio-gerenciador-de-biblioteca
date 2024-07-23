using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsFilteredByLibrary
{
    public record GetLoansDetailsFilteredByLibraryQuery(
        int Page,
        int Size,
        int LibraryId,
        string? BookName,
        string? UserName,
        DateTime LoanDate,
        DateTime LoanValidity,
        LoanStatus Status) : IRequest<CustomResponse<IEnumerable<LoanDetailsViewModel>>>;
}
