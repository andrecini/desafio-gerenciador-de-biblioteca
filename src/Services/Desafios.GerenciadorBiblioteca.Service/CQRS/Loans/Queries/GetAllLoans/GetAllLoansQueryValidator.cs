using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetAllLoans
{
    public class GetAllLoansQueryValidator : AbstractValidator<GetAllLoansQuery>
    {
        public GetAllLoansQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(0)
                .WithMessage("A Página deve ser maior ou igual a 0.");

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(0)
                .WithMessage("O Tamanho da Página deve ser maior ou igual a 0.");
        }
    }
}
