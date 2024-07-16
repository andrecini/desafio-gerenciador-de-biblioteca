using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetLoanById
{
    public record GetLoanByIdQuery(int Id) : IRequest<Loan>;
}
