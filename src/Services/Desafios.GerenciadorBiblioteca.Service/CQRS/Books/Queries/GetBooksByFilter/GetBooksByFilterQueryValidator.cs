using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBooksByFilter
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
        }
    }
}
