using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBookById
{
    public record GetBookByIdQuery(int Id) : IRequest<CustomResponse<Book>>;
}
