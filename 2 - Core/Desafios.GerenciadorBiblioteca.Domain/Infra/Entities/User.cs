using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities.Base;

namespace Desafios.GerenciadorBiblioteca.Domain.Infra.Entities
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
