namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Requests
{
    public record LoanDTO(int InventoryId, int UserId, DateTime LoanDate, DateTime LoanValidity)
    {
    }
}
