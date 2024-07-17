using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Users.Commands.AddUser
{
    public record AddUserCommand(string Name, string Email, string Phone, string Password) : IRequest<UserViewModel>;
}
