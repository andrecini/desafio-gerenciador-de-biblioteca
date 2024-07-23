using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetUserByName
{
    public record GetUsersByNameQuery(int Page, int Size, string? Name) : IRequest<CustomResponse<IEnumerable<UserViewModel>>>;
}
