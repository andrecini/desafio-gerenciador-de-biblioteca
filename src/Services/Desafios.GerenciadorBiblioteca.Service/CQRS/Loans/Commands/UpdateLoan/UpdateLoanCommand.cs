using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.UpdateLoan
{
    public record UpdateLoanCommand(int Id, int InventoryId, int UserId, DateTime LoanDate, DateTime LoanValidity) : IRequest<Loan>;
}
