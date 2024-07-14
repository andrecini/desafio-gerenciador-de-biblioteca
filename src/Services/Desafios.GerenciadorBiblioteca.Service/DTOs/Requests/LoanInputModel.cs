namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Requests
{
    public record LoanInputModel(int InventoryId, int UserId, DateTime LoanDate, DateTime LoanValidity);
}
