using Desafios.GerenciadorBiblioteca.Service.DTOs.InputModels;
using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Validators
{
    public class InventoryValidator : AbstractValidator<InventoryInputModel>
    {
        public InventoryValidator()
        {
            RuleFor(b => b.LibraryId)
                .GreaterThan(0)
                .WithMessage("Id da biblioteca inválido.");

            RuleFor(b => b.BookId)
                .GreaterThan(0)
                .WithMessage("Id do livro inválido.");
        }
    }
}
