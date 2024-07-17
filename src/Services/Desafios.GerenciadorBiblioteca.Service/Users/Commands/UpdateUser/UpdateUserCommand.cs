using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(int Id, string Name, string Email, string Phone) : IRequest<UserViewModel>;
}
