using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Validators
{
    public class LoanValidator : AbstractValidator<LoanDTO>
    {
        public LoanValidator()
        {
            RuleFor(b => b.LibraryId)
                .GreaterThan(0)
                .WithMessage("Id da biblioteca inválido.");

            RuleFor(b => b.BookId)
                .GreaterThan(0)
                .WithMessage("Id do livro inválido.");

            RuleFor(b => b.UserId)
                .GreaterThan(0)
                .WithMessage("Id do usuário inválido.");

            RuleFor(b => b.LoanValidity)
                .GreaterThan(DateTime.Now.AddDays(1))
                .WithMessage("A Data de Validade deve ser no mínimo de 24h após o horário atual.");

            RuleFor(b => b.LoanDate)
                .LessThan(DateTime.Now)
                .WithMessage("A Data de Empréstimo deve ser menor que o horário atual.");
        }
    }
}
