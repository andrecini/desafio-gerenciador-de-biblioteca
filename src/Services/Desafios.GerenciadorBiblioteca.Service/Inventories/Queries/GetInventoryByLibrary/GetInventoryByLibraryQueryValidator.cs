using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafios.GerenciadorBiblioteca.Service.Inventories.Queries.GetInventoryByLibrary
{
    public class GetInventoryByLibraryQueryValidator : AbstractValidator<GetInventoryByLibraryQuery>
    {
        public GetInventoryByLibraryQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("A Página deve ser maior ou igual a 1.");

            RuleFor(x => x.Size)
                .GreaterThanOrEqualTo(5)
                .WithMessage("O Tamanho da Página deve ser maior ou igual a 5.");

            RuleFor(x => x.LibraryId)
                .GreaterThanOrEqualTo(5)
                .WithMessage("O Id da Biblioteca deve ser maior ou igual a 5.");
        }
    }
}
