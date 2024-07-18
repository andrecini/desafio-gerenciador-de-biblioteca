using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBooksDetailsByFilter
{
    public class GetBooksDetailsByFilterQueryValidator : AbstractValidator<GetBooksDetailsByFilterQuery>
    {
        public GetBooksDetailsByFilterQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A Página deve ser maior ou igual a 1.");

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(5)
                .WithMessage("O Tamanho da Página deve ser maior ou igual a 5.");

            RuleFor(x => x.LibraryId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id da Biblioteca deve ser igual ou maior do que 1.");
        }
    }
}
