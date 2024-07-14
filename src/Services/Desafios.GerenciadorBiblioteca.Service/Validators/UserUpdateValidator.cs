using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Validators
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateInputModel>
    {
        public UserUpdateValidator()
        {
            RuleFor(b => b.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Nome do Usuário é obrigatório.");

            RuleFor(b => b.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Email é obrigatório.");

            RuleFor(b => b.Email)
            .EmailAddress()
            .WithMessage("O Email está fora do padrão.");

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
