﻿using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetAllLoans
{
    public class GetAllLoansQueryValidator : AbstractValidator<GetAllLoansQuery>
    {
        public GetAllLoansQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A Página deve ser maior ou igual a 1.");

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(5)
                .WithMessage("O Tamanho da Página deve ser maior ou igual a 5.");
        }
    }
}
