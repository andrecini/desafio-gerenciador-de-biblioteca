using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Users.Commands.RemoveUser
{
    public record RemoveUserCommand(int Id) : IRequest<bool>;
}
