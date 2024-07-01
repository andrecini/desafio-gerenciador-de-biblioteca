namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Requests
{
    public record InventoryDTO(int LibraryId, int BookId, bool Available = true)
    {
    }
}
