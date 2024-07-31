using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserByEmail
{
    public record GetUserByEmailQuery(string Email) : IRequest<CustomResponse<UserViewModel>>;
}
