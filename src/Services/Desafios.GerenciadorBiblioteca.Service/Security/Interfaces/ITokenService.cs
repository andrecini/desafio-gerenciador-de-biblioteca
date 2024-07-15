using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Security;

namespace Desafios.GerenciadorBiblioteca.Service.Security.Interfaces
{
    public interface ITokenService
    {
        TokenModel GenerateJwtToken(int id, string name, Roles role);
    }
}
