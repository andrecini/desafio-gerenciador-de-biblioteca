using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Services.Base;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;

namespace Desafios.GerenciadorBiblioteca.Domain.Services
{
    public interface IUserService : IService<UserDTO, User>
    {
        Task<IEnumerable<User>> FindAsync(string name);
    }
}
