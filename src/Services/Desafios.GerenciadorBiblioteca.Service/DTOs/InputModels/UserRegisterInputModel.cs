namespace Desafios.GerenciadorBiblioteca.Service.DTOs.InputModels
{
    public record UserRegisterInputModel(string? Name, string? Email, string? Phone, string? Password);
}
