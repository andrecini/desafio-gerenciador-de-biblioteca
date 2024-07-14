namespace Desafios.GerenciadorBiblioteca.Service.DTOs.Requests
{
    public record UserRegisterInputModel(string? Name, string? Email, string? Phone, string? Password);
}
