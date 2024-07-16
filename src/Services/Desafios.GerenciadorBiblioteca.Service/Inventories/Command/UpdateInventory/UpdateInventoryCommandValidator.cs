using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Command.UpdateInventory
{
    public class UpdateInventoryCommandValidator : AbstractValidator<UpdateInventoryCommand>
    {
        public UpdateInventoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id deve ser maior ou igual a 1.");

            RuleFor(x => x.LibraryId)
               .GreaterThanOrEqualTo(1)
               .WithMessage("O Id da Biblioteca deve ser maior ou igual a 1.");

            RuleFor(x => x.BookId)
               .GreaterThanOrEqualTo(1)
               .WithMessage("O Id do Livro deve ser maior ou igual a 1.");
        }
    }
}
