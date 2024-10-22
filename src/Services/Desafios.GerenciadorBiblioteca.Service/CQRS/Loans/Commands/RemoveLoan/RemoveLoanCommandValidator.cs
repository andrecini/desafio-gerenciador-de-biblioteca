﻿using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.RemoveLoan
{
    public class RemoveLoanCommandValidator : AbstractValidator<RemoveLoanCommand>
    {
        public RemoveLoanCommandValidator()
        {
            RuleFor(b => b.Id)
                .GreaterThan(0)
                .WithMessage("Id do Invenmtário inválido.");
        }
    }
}
