using Desafios.GerenciadorBiblioteca.Domain.Application.Enums;

namespace Desafios.GerenciadorBiblioteca.Domain.Application.Entities.Inventory
{
    public class InventoryFilter
    {
        public int LibraryId { get; set; }
        public int BookId { get; set; }
        public InventoryStatus Status { get; set; }
    }
}
