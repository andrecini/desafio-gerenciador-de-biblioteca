using Desafios.GerenciadorBiblioteca.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public class Book : IEntity<int>
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public int? Year { get; set; }

        [JsonIgnore]
        public ICollection<Inventory>? Inventories { get; set; }
    }
}
