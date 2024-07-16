using FluentValidation;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Commands.UpdateLoanStatus
{
    public class UpdateLoanStatusCommandValidator : AbstractValidator<UpdateLoanStatusCommand>
    {
        public UpdateLoanStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithMessage("O Id deve ser maior ou igual a 1.");
        }
    }
}
