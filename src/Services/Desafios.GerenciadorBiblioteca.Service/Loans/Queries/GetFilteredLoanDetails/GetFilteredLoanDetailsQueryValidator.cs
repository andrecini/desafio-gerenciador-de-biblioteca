using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetFilteredLoanDetails
{
    public class GetFilteredLoanDetailsQueryValidator : AbstractValidator<GetFilteredLoanDetailsQuery>
    {
        public GetFilteredLoanDetailsQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A Página deve ser maior ou igual a 1.");

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(5)
                .WithMessage("O Tamanho da Página deve ser maior ou igual a 5.");

            RuleFor(x => x.InventoryId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id do Inventário deve ser maior ou igual a 1.");

            RuleFor(x => x.UserId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id do Usuário deve ser maior ou igual a 5.");
        }
    }
}
