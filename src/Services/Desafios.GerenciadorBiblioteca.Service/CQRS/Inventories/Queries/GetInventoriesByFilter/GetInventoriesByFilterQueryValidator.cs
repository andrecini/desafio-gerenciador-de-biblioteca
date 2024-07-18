using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoriesByFilter
{
    public class GetInventoriesByFilterQueryValidator : AbstractValidator<GetInventoriesByFilterQuery>
    {
        public GetInventoriesByFilterQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A Página deve ser maior ou igual a 1.");

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(5)
                .WithMessage("O Tamanho da Página deve ser maior ou igual a 5.");

            RuleFor(x => x.LibraryId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A Id da Biblioteca deve ser maior ou igual a 1.");

            RuleFor(x => x.BookId)
               .GreaterThanOrEqualTo(1)
               .WithMessage("A Id do Livro deve ser maior ou igual a 1.");
        }
    }
}
