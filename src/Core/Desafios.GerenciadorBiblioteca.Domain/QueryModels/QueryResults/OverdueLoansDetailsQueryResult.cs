using Desafios.GerenciadorBiblioteca.Domain.Entities;

namespace Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryResults
{
    public record OverdueLoansDetailsQueryResult(
        DateTime LoanDate,
        DateTime LoanValidity,
        string BookName,
        string Username, 
        string Email);
}