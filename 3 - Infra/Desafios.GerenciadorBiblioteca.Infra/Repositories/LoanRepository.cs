﻿using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Repositories;
using Desafios.GerenciadorBiblioteca.Infra.Context;

namespace Desafios.GerenciadorBiblioteca.Infra.Repositories
{
    public class LoanRepository(LibraryDbContext context) : GenericRepository<Loan, int>(context), ILoanRepository
    {
    }
}