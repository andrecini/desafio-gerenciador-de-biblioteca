using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoryById
{
    public class GetInventoryByIdQueryValidator : AbstractValidator<GetInventoryByIdQuery>
    {
        public GetInventoryByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A Id deve ser maior ou igual a 1.");
        }
    }
}
