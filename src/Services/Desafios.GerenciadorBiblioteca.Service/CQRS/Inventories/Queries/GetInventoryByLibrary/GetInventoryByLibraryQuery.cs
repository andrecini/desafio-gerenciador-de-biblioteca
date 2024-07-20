using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Inventories.Queries.GetInventoryByLibrary
{
    public record GetInventoryByLibraryQuery(int Page, int Size, int LibraryId) : IRequest<CustomResponse<IEnumerable<Inventory>>>;
}