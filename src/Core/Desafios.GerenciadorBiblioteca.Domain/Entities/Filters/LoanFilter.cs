using Desafios.GerenciadorBiblioteca.Domain.Enums;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities.Filters
{
    public class LoanFilter
    {
        public int InventoryId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime LoanValidity { get; set; }
        public LoanStatus Status { get; set; }
    }
}
