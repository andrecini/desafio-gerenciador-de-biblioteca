using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Commands.RemoveLibrary
{
    public record RemoveLibraryCommand(int Id) : IRequest<CustomResponse<bool>>;
}
