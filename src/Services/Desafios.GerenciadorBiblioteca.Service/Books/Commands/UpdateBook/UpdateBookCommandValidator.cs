using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Commands.UpdateBook
{
    internal class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id do Livro deve ser maior ou igual a 1.");

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Título do Livro é obrigatório.");

            RuleFor(x => x.Author)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Nome do Autor é obrigatório.");

            RuleFor(x => x.ISBN)
                .Matches(@"^\d+$")
                .WithMessage("Digite apenas números no ISBN.");

            RuleFor(x => x.Year)
                .GreaterThan(0)
                .LessThan(DateTime.Now.Year)
                .WithMessage($"O ano deve ser maior do que 0 e menor do que {DateTime.Now.Year}.");
        }
    }
}
