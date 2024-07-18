using Desafios.GerenciadorBiblioteca.Domain.Entities;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Commands.AddLibrary
{
    public record AddLibraryCommand(string Name, string CNPJ, string Phone) : IRequest<Library>;
}
