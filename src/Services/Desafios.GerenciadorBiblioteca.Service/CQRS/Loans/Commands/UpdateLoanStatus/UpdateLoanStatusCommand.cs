using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.UpdateLoanStatus
{
    public record UpdateLoanStatusCommand(int Id, bool Returned) : IRequest<CustomResponse<Loan>>;
}
