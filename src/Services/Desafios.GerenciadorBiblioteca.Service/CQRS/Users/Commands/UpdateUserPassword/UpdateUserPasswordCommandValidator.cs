using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id deve ser maior ou igual a 1.");

            RuleFor(x => x.Id)
               .NotNull()
               .NotEmpty()
               .WithMessage("A Nova Senha é obrigatória.");
        }
    }
}
