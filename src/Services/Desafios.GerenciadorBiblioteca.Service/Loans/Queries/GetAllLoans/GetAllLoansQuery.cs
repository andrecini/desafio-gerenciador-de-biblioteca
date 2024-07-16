using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetAllLoans
{
    public record GetAllLoansQuery(int Page, int Size) : IRequest<IEnumerable<Loan>>;
}
