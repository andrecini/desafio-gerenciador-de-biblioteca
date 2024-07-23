using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansByUser
{
    public record GetLoansByUserQuery(int Page, int Size, int UserId) : IRequest<CustomResponse<IEnumerable<Loan>>>;
}
