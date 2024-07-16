using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Loans.Commands.RemoveLoan
{
    public record RemoveLoanCommand(int Id) : IRequest<bool>;
}
