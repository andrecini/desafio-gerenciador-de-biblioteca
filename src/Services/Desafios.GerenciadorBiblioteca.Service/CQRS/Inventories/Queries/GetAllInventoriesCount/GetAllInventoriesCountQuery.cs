using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetAllInventoriesCount
{
    public record GetAllInventoriesCountQuery() : IRequest<int>;
}
