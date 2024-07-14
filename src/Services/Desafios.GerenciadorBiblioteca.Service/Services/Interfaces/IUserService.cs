using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;

namespace Desafios.GerenciadorBiblioteca.Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserViewModel> GetByIdAsync(int id);
        Task<UserViewModel> AddAsync(UserRegisterInputModel dto);
        Task<UserViewModel> UpdateAsync(int id, UserUpdateInputModel dto);
        Task<bool> RemoveAsync(int id);
        Task<IEnumerable<UserViewModel>> GetByNameAsync(string name);
    }
}
