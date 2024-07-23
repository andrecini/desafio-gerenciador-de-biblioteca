using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.RemoveUser
{
    public record RemoveUserCommand(int Id) : IRequest<bool>;
}
