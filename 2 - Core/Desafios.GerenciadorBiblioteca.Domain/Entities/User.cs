using System.ComponentModel.DataAnnotations;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public class User : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
