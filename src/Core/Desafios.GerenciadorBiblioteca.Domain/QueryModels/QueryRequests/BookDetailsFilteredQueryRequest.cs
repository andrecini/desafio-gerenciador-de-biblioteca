namespace Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests
{
    public record BookDetailsFilteredQueryRequest(
        int LibraryId,
        string Title,
        string Author,
        string ISBN,
        int Year,
        int Available);
}
