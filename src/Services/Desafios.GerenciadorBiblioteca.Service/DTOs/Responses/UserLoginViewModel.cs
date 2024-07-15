using Desafios.GerenciadorBiblioteca.Service.DTOs.Security;

namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Responses
{
    public record UserLoginViewModel (UserViewModel User, TokenModel Token);
}
