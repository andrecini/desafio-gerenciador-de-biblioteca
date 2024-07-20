using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsFiltered
{
    public record GetLoansDetailsFilteredQuery(
        int Page,
        int Size,
        int LibraryId,
        string? BookName,
        string? UserName,
        DateTime LoanDate,
        DateTime LoanValidity,
        LoanStatus Status) : IRequest<CustomResponse<IEnumerable<LoanDetailsViewModel>>>;
}
