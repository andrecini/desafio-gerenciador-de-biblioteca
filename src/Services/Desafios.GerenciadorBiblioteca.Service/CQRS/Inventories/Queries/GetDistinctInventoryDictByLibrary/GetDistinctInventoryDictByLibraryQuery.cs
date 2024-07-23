using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetDistinctInventoryDictByLibrary
{
    public record GetDistinctInventoryDictByLibraryQuery(int LibraryId) : IRequest<CustomResponse<Dictionary<int, string>>>;
}