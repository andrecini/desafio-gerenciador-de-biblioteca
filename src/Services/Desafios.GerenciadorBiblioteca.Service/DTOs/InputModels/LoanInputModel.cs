namespace Desafios.GerenciadorBiblioteca.Service.DTOs.InputModels
{
    public record LoanInputModel(int InventoryId, int UserId, DateTime LoanDate, DateTime LoanValidity);
}
