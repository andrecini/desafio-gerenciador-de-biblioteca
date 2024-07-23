using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoanById
{
    public record GetLoanByIdQuery(int Id) : IRequest<CustomResponse<Loan>>;
}
