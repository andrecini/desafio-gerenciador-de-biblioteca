using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoryDictByLibrary
{
    public record GetInventoryDictByLibraryQuery(int LibraryId) : IRequest<CustomResponse<Dictionary<int, string>>>;
}