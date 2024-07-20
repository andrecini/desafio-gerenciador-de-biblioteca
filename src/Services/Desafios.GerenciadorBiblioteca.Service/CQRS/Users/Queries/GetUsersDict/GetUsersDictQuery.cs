using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUsersDict
{
    public record GetUsersDictQuery() : IRequest<CustomResponse<Dictionary<int, string>>>;
}
