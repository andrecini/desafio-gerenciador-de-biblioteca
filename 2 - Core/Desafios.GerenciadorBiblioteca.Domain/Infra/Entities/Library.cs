using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities.Base;

namespace Desafios.GerenciadorBiblioteca.Domain.Infra.Entities
{
    public class Library : IEntity<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CNPJ { get; set; }
        public string? Prone { get; set; }

        public ICollection<Inventory>? Inventories { get; set; }
        public ICollection<Loan>? Loans { get; set; }
    }
}
