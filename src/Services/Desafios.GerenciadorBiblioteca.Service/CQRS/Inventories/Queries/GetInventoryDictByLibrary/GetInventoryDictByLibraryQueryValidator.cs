using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoryDictByLibrary
{
    public class GetInventoryDictByLibraryQueryValidator : AbstractValidator<GetInventoryDictByLibraryQuery>
    {
        public GetInventoryDictByLibraryQueryValidator()
        {
            RuleFor(x => x.LibraryId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id da Biblioteca deve ser maior ou igual a 1.");
        }
    }
}
