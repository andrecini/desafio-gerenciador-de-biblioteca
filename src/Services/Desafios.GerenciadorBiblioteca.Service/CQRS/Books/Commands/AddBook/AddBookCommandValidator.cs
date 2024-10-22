﻿using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Commands.AddBook
{
    public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookCommandValidator()
        {
            RuleFor(b => b.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Título do Livro é obrigatório.");

            RuleFor(b => b.Author)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Nome do Autor é obrigatório.");

            RuleFor(b => b.ISBN)
                .Matches(@"^\d+$")
                .WithMessage("Digite apenas números no ISBN.");

            RuleFor(b => b.Year)
                .GreaterThan(0)
                .LessThan(DateTime.Now.Year)
                .WithMessage($"O ano deve ser maior do que 0 e menor do que {DateTime.Now.Year}.");
        }
    }
}
