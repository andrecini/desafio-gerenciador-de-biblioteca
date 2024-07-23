using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UpdateUserPassword
{
    public record UpdateUserPasswordCommand(int Id, string NewPassword) : IRequest<CustomResponse<UserViewModel>>;
}
