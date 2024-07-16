using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Queries.GetBookById
{
    public record GetBookByIdQuery(int Id) : IRequest<Book>;
}
