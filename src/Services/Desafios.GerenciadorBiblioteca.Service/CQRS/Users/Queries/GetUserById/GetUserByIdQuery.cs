using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(int Id) : IRequest<UserViewModel>;
}
