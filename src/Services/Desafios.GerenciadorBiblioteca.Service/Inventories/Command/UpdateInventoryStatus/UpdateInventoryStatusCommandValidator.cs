using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Command.UpdateInventoryStatus
{
    public class UpdateInventoryStatusCommandValidator : AbstractValidator<UpdateInventoryStatusCommand>
    {
        public UpdateInventoryStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id deve ser maior ou igual a 1.");
        }
    }
}
