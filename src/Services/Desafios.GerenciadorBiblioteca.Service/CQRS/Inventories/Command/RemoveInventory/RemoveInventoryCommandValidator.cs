using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Command.RemoveInventory
{
    public class RemoveInventoryCommandValidator : AbstractValidator<RemoveInventoryCommand>
    {
        public RemoveInventoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Ide deve ser maior ou igual a 1.");
        }
    }
}
