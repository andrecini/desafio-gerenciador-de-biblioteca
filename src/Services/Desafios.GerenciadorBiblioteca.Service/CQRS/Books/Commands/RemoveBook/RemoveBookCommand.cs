using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Commands.RemoveBook
{
    public record RemoveBookCommand(int Id) : IRequest<CustomResponse<bool>>;
}
