using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(int Id) : IRequest<CustomResponse<UserViewModel>>;
}
