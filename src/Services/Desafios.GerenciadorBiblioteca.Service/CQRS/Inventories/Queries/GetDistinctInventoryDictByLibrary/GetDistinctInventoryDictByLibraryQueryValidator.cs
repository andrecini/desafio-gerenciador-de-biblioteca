using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetDistinctInventoryDictByLibrary
{
    public class  GetDistinctInventoryDictByLibraryQueryValidator : AbstractValidator<GetDistinctInventoryDictByLibraryQuery>
    {
        public  GetDistinctInventoryDictByLibraryQueryValidator()
        {
            RuleFor(x => x.LibraryId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id da Biblioteca deve ser maior ou igual a 1.");
        }
    }
}
