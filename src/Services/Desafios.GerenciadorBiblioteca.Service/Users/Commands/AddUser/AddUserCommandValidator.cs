﻿using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Users.Commands.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
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

            RuleFor(b => b.Password)
                .NotNull().NotEmpty()
                .WithMessage("A Senha é obrigatória.")
                .Length(8)
                .WithMessage("A Senha deve ter no mínimo 8 caracteres e pelo menos 1 símbolo.");
        }
    }
}