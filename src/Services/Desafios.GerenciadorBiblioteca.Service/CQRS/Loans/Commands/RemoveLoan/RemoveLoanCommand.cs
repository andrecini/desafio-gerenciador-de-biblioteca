using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.RemoveLoan
{
    public record RemoveLoanCommand(int Id) : IRequest<bool>;
}
