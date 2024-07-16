using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Commands.RemoveBook
{
    public record RemoveBookCommand(int Id) : IRequest<bool>;
}
