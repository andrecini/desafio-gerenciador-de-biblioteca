namespace Desafios.GerenciadorBiblioteca.Service.Security.Interfaces
{
    public interface IHangfireApiManager
    {
        Task GetAsync(int userId, string email);
    }
}
