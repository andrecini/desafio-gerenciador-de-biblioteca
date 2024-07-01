using Desafios.GerenciadorBiblioteca.Domain.Enums;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities.Filters
{
    public class LoanFilter
    {
        public int LibraryId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime LoanValidity { get; set; }
        public LoanStatus Status { get; set; }
    }
}
