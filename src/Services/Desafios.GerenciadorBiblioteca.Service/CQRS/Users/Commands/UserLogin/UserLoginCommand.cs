using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UserLogin
{
    public record UserLoginCommand(string Email, string Password) : IRequest<CustomResponse<UserViewModel>>;
}
