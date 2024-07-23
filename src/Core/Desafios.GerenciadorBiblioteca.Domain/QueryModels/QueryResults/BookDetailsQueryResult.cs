namespace Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryResults
{
    public record BookDetailsQueryResult
    (
        int Id,
        string Title,
        string Author,
        string ISBN,
        int Year,
        int InventoryId,
        bool Available
    );
}
