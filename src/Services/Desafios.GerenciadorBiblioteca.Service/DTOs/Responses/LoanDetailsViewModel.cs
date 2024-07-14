using Desafios.GerenciadorBiblioteca.Domain.Entities;

namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Responses
{
    public record LoanDetailsViewModel(Loan LoanDetails, string BookName, string Username);
}