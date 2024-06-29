using System.ComponentModel.DataAnnotations;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public class Book : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public int? Year { get; set; }
    }
}
