using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(int Id, string Name, string Email, string Phone) : IRequest<CustomResponse<UserViewModel>>;
}
