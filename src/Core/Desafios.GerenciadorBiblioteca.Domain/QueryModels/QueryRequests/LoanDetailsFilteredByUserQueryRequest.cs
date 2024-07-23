using Desafios.GerenciadorBiblioteca.Domain.Enums;

namespace Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests
{
    public record LoanDetailsFilteredByUserQueryRequest(
        int Page,
        int Size,
        int UserId,
        string BookName,
        DateTime LoanDate,
        DateTime LoanValidity,
        LoanStatus Status);
}
