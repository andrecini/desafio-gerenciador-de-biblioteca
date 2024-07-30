namespace Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels
{
    public record OverdueLoansDetailsViewModel(DateTime LoanDate,
        DateTime LoanValidity,
        string BookName,
        string Username,
        string Email);
}
