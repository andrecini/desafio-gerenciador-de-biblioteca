using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Libraries.Commands.UpdateLibrary
{
    public class UpdateLibraryCommandValidator : AbstractValidator<UpdateLibraryCommand>
    {
        public UpdateLibraryCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id deve ser maior ou igual a 1.");

            RuleFor(b => b.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Nome da Biblioteca é obrigatório.");

            RuleFor(b => b.CNPJ)
                .NotNull()
                .NotEmpty()
                .WithMessage("O CNPJ é obrigatório.");

            RuleFor(b => b.CNPJ)
                .Matches(@"^\d+$")
                .WithMessage("Digite apenas números no CNPJ.");

            RuleFor(b => b.Phone)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Número de Telefone é obrigatório.");

            RuleFor(b => b.Phone)
                .Matches(@"^\d+$")
                .WithMessage("Digite apenas números no Número de Telefone.");
        }
    }
}
