﻿using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Commands.AddLoan
{
    public record AddLoanCommand(int InventoryId, int UserId, DateTime LoanDate, DateTime LoanValidity) : IRequest<Loan>;
}
