using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserByName
{
    public record GetUsersByNameQuery(int Page, int Size, string? Name) : IRequest<IEnumerable<UserViewModel>>;
}
