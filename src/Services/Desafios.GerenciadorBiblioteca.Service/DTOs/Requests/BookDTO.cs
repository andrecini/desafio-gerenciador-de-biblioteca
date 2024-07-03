namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Requests
{
    public record BookDTO(string? Title, string? Author, string? ISBN, int? Year)
    {
    }
}
