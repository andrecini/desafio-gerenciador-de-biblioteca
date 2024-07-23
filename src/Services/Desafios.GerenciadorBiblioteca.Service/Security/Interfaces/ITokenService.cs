using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;

namespace Desafios.GerenciadorBiblioteca.Service.Security.Interfaces
{
    public interface ITokenService
    {
        TokenViewModel GenerateJwtToken(int id, string name, Roles role);
    }
}
