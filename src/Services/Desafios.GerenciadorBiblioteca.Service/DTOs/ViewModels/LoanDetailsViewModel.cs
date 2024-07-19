namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Responses
{
    public record LoanDetailsViewModel(
        int Id,
        int InventoryId,
        int UserId,
        DateTime LoanDate,
        DateTime LoanValidity,
        bool Returned,
        string BookName,
        string Username);
}