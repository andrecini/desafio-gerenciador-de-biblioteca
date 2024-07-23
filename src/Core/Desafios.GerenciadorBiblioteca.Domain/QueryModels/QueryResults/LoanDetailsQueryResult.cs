using Desafios.GerenciadorBiblioteca.Domain.Entities;

namespace Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryResults
{
    public record LoanDetailsQueryResult(
        int Id,
        int InventoryId,
        int UserId,
        DateTime LoanDate,
        DateTime LoanValidity,
        bool Returned,
        string BookName,
        string Username);
}