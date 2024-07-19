using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Queries.GetAllUsersCount
{
    public record GetAllUsersCountQuery() : IRequest<int>;
}
