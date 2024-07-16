using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetLoansDetailsByLibrary
{
    public record GetLoansDetailsByLibraryQuery(int Page, int Size, int LibraryId) : IRequest<IEnumerable<LoanDetailsViewModel>>;
}
