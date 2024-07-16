using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBooksByFilter
{
    public class GetBooksByFilterQueryValidator : AbstractValidator<GetBooksByFilterQuery>
    {
        public GetBooksByFilterQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A Página deve ser maior ou igual a 1.");

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(5)
                .WithMessage("O Tamanho da Página deve ser maior ou igual a 5.");

            RuleFor(x => x.Year)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Ano deve ser maior ou igual a 1.");
        }
    }
}
