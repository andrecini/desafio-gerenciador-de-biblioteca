using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetFilteredLoanDetails
{
    public class GetLoansDetailsFilteredQueryValidator : AbstractValidator<GetLoansDetailsFilteredQuery>
    {
        public GetLoansDetailsFilteredQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A Página deve ser maior ou igual a 1.");

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(5)
                .WithMessage("O Tamanho da Página deve ser maior ou igual a 5.");
        }
    }
}
