using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.AddUser
{
    public record AddUserCommand(string Name, string Email, string Phone, string Password) : IRequest<CustomResponse<UserViewModel>>;
}
