﻿using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetLibrariesByBookFiltered
{
    public class GetLibrariesByBookFilteredQueryValidator : AbstractValidator<GetLibrariesByBookFilteredQuery>
    {
        public GetLibrariesByBookFilteredQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A Página deve ser maior ou igual a 1.");

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(5)
                .WithMessage("O Tamanho da Página deve ser maior ou igual a 5.");

            RuleFor(x => x.BookId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id do Livro deve ser maior ou igual a 1.");
        }
    }
}