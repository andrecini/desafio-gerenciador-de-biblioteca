using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetLoansDetailsByLibrary
{
    public class GetLoansDetailsByLibraryQueryValidator : AbstractValidator<GetLoansDetailsByLibraryQuery>
    {
        public GetLoansDetailsByLibraryQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A Página deve ser maior ou igual a 1.");

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(5)
                .WithMessage("O Tamanho da Página deve ser maior ou igual a 5.");

            RuleFor(x => x.LibraryId)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id da Biblioteca deve ser maior ou igual a 1.");
        }
    }
}
