using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetLibraryById
{
    public class GetLibraryByIdQueryValidator : AbstractValidator<GetLibraryByIdQuery>
    {
        public GetLibraryByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id deve ser maior ou igual a 1.");
        }
    }
}
