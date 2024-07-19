using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetAllLoansCount
{
    public record GetAllLoansCountQuery() : IRequest<int>;
}
