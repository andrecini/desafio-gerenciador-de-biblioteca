namespace Desafios.GerenciadorBiblioteca.Domain.Models.Filters
{
    public class BookFilter
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
        public int Year { get; set; }
    }
}
