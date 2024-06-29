using System.ComponentModel.DataAnnotations;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public class Library : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CNPJ { get; set; }
        public string? Prone { get; set; }
    }
}
