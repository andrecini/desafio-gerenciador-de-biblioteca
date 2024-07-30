using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailQueryValidator()
        {
            RuleFor(b => b.Email)
               .NotNull()
               .NotEmpty()
               .WithMessage("O Email é obrigatório.")
               .EmailAddress()
               .WithMessage("O Email está fora do padrão.");
        }
    }
}
