using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetAllUsers
{
    public record GetAllUsersQuery(int Page, int Size) : IRequest<CustomResponse<IEnumerable<UserViewModel>>>;
}
