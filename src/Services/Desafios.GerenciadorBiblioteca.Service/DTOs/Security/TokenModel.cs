using Desafios.GerenciadorBiblioteca.Domain.Enums;

namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Security
{
    public record TokenModel(string? Id, string? Name, string? Token, Roles Role, DateTime ValidTo);
}
