using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.VerificationCodes.Commands.ValidateVerificationCode
{
    public class ValidateVerificationCodeCommandValidator : AbstractValidator<ValidateVerificationCodeCommand>
    {
        public ValidateVerificationCodeCommandValidator()
        {
            RuleFor(b => b.UserId)
              .GreaterThanOrEqualTo(1)
              .WithMessage("O Id do Usuário deve ser maior ou igual a 1.");

            RuleFor(b => b.Code)
              .NotNull()
              .NotEmpty()
              .Length(6)
              .Matches(@"^\d+$")
              .WithMessage("O Código de confirmação dever ter 6 dígitos e somente números!");
        }
    }
}
