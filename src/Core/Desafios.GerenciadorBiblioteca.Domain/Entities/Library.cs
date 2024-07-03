using Desafios.GerenciadorBiblioteca.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public class Library : IEntity<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CNPJ { get; set; }
        public string? Phone { get; set; }

        [JsonIgnore]
        public ICollection<Inventory>? Inventories { get; set; }
    }
}
