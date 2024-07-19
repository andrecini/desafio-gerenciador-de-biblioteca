using Desafios.GerenciadorBiblioteca.Domain.Enums;

namespace Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels
{
    public record TokenViewModel(int Id, string Name, string Token, Roles Role, DateTime ValidTo);
}
