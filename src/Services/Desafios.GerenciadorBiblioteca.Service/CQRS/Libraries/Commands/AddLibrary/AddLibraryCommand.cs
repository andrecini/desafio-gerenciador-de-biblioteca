using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Commands.AddLibrary
{
    public record AddLibraryCommand(string Name, string CNPJ, string Phone) : IRequest<CustomResponse<Library>>;
}
