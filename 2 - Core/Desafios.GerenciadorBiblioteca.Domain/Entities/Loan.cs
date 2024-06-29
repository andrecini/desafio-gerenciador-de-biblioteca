using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public class Loan : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Library")]
        public int LibraryId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime LoanValidity { get; set; }

        public bool Returned { get; set; }
    }
}
