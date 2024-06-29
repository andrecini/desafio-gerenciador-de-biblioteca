using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public class Invetory : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Library")]
        public int LibraryId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public int Amount { get; set; }

        public int Available { get; set; }
    }
}
