using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.UpdateLoanStatus
{
    public record UpdateLoanStatusCommand(int Id, bool Returned) : IRequest<Loan>;
}
