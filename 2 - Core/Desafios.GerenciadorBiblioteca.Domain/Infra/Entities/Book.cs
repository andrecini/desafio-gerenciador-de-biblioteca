using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities.Base;

namespace Desafios.GerenciadorBiblioteca.Domain.Infra.Entities
{
    public class Book : IEntity<int>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public int? Year { get; set; }

        public ICollection<Inventory>? Inventories { get; set; }
        public ICollection<Loan>? Loans { get; set; }
    }
}
