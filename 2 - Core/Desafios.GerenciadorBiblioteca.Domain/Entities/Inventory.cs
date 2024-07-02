using Desafios.GerenciadorBiblioteca.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public class Inventory : IEntity<int>
    {
        public int Id { get; set; }
        public int LibraryId { get; set; }
        public int BookId { get; set; }
        public bool Available { get; set; }

        [JsonIgnore]
        public Library? Library { get; set; }
        [JsonIgnore]
        public Book? Book { get; set; }
    }
}
