using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetAllLibraries
{
    public class GetAllLibrariesValidator : AbstractValidator<GetAllLibrariesQuery>
    {
        public GetAllLibrariesValidator()
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
