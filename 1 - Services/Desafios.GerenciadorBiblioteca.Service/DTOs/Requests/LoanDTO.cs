namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Requests
{
    public record LoanDTO(int LibraryId, int UserId, int BookId, DateTime LoanDate, DateTime LoanValidity)
    {
    }
}
