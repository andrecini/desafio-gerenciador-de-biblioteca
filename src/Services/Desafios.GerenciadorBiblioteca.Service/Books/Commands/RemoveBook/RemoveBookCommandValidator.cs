using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Commands.RemoveBook
{
    public class RemoveBookCommandValidator : AbstractValidator<RemoveBookCommand>
    {
        public RemoveBookCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id do Livro deve ser maior ou igual a 1.");
        }
    }
}
