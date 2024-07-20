using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetAllLoans
{
    public record GetAllLoansQuery(int Page, int Size) : IRequest<CustomResponse<IEnumerable<Loan>>>;
}
