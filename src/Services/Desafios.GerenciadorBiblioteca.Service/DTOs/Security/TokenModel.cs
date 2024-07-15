using Desafios.GerenciadorBiblioteca.Domain.Enums;

namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Security
{
    public record TokenModel(int Id, string Name, string Token, Roles Role, DateTime ValidTo);
}
