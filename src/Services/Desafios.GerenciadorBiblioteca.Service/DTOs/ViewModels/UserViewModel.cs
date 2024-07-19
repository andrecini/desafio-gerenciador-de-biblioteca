using Desafios.GerenciadorBiblioteca.Domain.Enums;

namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Responses
{
    public record UserViewModel(int Id, string Name, string Email, string Phone, Roles Role);
}
