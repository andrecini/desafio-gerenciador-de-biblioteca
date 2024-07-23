using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.RemoveLoan
{
    public record RemoveLoanCommand(int Id) : IRequest<CustomResponse<bool>>;
}
