using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUsersDict
{
    public record GetUsersDictQuery() : IRequest<CustomResponse<Dictionary<int, string>>>;
}
