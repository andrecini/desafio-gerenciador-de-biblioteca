using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Libraries.Commands.RemoveLibrary
{
    public record RemoveLibraryCommand(int Id) : IRequest<bool>;
}
