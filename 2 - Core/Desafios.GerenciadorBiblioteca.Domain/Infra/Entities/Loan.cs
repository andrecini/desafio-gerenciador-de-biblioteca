using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities.Base;

namespace Desafios.GerenciadorBiblioteca.Domain.Infra.Entities
{
    public class Loan : IEntity<int>
    {
        public int Id { get; set; }
        public int LibraryId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime LoanValidity { get; set; }
        public bool Returned { get; set; }

        public User? User { get; set; }
        public Book? Book { get; set; }
        public Library? Library { get; set; }
    }
}
