using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.AddLoan
{
    public record AddLoanCommand(int InventoryId, int UserId, DateTime LoanDate, DateTime LoanValidity) : IRequest<CustomResponse<Loan>>;
}
