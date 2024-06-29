namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public class User : IEntity<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public ICollection<Loan>? Loans { get; set; }
    }
}
