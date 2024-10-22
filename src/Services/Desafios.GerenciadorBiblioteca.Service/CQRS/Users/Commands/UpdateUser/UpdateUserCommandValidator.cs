﻿using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(b => b.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Nome do Usuário é obrigatório.");

            RuleFor(b => b.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Email é obrigatório.")
                .EmailAddress()
                .WithMessage("O Email está fora do padrão.");


            RuleFor(b => b.Phone)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Número de Telefone é obrigatório.")
                .Matches(@"^\d+$")
                .WithMessage("Digite apenas números no Número de Telefone.");
        }
    }
}
