using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;

namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Responses
{
    public record UserLoginViewModel (UserViewModel User, TokenViewModel Token);
}
