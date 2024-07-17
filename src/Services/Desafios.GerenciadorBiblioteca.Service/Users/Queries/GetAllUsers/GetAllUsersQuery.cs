using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Users.Queries.GetAllUsers
{
    public record GetAllUsersQuery(int Page, int Size) : IRequest<IEnumerable<UserViewModel>>;
}
