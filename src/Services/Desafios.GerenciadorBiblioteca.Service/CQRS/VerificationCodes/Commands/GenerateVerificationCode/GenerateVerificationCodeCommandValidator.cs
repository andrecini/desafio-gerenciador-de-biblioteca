using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.VerificationCodes.Commands.GenerateVerificationCode
{
    public class GenerateVerificationCodeCommandValidator : AbstractValidator<GenerateVerificationCodeCommand>
    {
        public GenerateVerificationCodeCommandValidator()
        {
            RuleFor(b => b.UserId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id do Usuário deve ser maior ou igual a 1.");
        }
    }
}