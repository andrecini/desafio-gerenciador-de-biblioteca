using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetAllInventories
{
    public class GetAllInventoriesQueryValidator : AbstractValidator<GetAllInventoriesQuery>
    {
        public GetAllInventoriesQueryValidator()
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
