using Desafios.GerenciadorBiblioteca.Domain.Entities;

namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Responses
{
    public record BookDetailsViewModel {

        public BookDetailsViewModel() { }

        public BookDetailsViewModel(Book book, int inventoryId, bool available)
        {
            Id = book.Id;
            Title = book.Title;
            Author = book.Author;
            ISBN = book.ISBN;
            Year = book.Year;
            InventoryId = inventoryId;
            Available = available;
        }


        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int? Year { get; set; }
        public int InventoryId { get; set; }
        public bool Available { get; set; }
    }
}
