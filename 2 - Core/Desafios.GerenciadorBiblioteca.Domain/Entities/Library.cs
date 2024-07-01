using Desafios.GerenciadorBiblioteca.Domain.Entities.Base;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public class Library : IEntity<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CNPJ { get; set; }
        public string? Phone { get; set; }

        public ICollection<Inventory>? Inventories { get; set; }
        public ICollection<Loan>? Loans { get; set; }
    }
}
