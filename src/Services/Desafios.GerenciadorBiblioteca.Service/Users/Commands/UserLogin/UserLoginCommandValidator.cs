using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Users.Commands.UserLogin
{
    public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginCommandValidator()
        {
            RuleFor(b => b.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Email é obrigatório.")
                .EmailAddress()
                .WithMessage("O Email está fora do padrão.");

            RuleFor(b => b.Password)
                .NotNull().NotEmpty()
                .WithMessage("A Senha é obrigatória.")
                .Length(8)
                .WithMessage("A Senha deve ter no mínimo 8 caracteres e pelo menos 1 símbolo.");
        }
    }
}
