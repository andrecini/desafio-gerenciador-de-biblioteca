using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Users.Commands.UpdateUserPassword
{
    public record UpdateUserPasswordCommand(int Id, string NewPassword) : IRequest<UserViewModel>;
}
