using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Commands.UpdateLibrary
{
    public record UpdateLibraryCommand(int Id, string Name, string CNPJ, string Phone) : IRequest<CustomResponse<Library>>;
}
