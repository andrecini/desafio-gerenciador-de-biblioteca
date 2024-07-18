using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansByUser
{
    public record GetLoansByUserQuery(int Page, int Size, int UserId) : IRequest<IEnumerable<Loan>>;
}
