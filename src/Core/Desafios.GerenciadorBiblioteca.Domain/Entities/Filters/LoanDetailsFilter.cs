namespace Desafios.GerenciadorBiblioteca.Domain.Entities.Filters
{
    public class LoanDetailsFilter
    {
        public int LibraryId { get; set; }
        public LoanFilter LoanFilter { get; set; }
        public string BookName { get; set; }
        public string UserName { get; set; }
    }
}
