﻿using Desafios.GerenciadorBiblioteca.Domain.Enums;

namespace Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests
{
    public record LoanDetailsFilteredByLibraryQueryRequest(
        int Page,
        int Size,
        int LibraryId,
        string BookName,
        string UserName,
        DateTime LoanDate,
        DateTime LoanValidity,
        LoanStatus Status);
}
