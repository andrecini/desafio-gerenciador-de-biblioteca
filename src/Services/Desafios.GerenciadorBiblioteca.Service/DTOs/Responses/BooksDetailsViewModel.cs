using Desafios.GerenciadorBiblioteca.Domain.Entities;

namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Responses
{
    public record BookDetailsViewModel(Book BookDetails, int InventoryId, bool Available);
}
