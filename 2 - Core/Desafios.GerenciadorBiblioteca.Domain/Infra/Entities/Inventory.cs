using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities.Base;

namespace Desafios.GerenciadorBiblioteca.Domain.Infra.Entities
{
    public class Inventory : IEntity<int>
    {
        public int Id { get; set; }
        public int LibraryId { get; set; }
        public int BookId { get; set; }
        public bool Available { get; set; }

        public Library? Library { get; set; }
        public Book? Book { get; set; }
    }
}
