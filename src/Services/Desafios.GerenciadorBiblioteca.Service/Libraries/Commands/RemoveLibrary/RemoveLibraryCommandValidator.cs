using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Libraries.Commands.RemoveLibrary
{
    public class RemoveLibraryCommandValidator : AbstractValidator<RemoveLibraryCommand>
    {
        public RemoveLibraryCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id deve ser maior ou igual a 1.");
        }
    }
}
