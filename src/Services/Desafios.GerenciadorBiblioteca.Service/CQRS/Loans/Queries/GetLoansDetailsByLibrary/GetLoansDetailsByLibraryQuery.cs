using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsByLibrary
{
    public record GetLoansDetailsByLibraryQuery(int Page, int Size, int LibraryId) : IRequest<CustomResponse<IEnumerable<LoanDetailsViewModel>>>;
}
