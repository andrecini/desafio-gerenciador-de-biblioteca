using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;

namespace Desafios.GerenciadorBiblioteca.Service.Services.Interfaces
{
    public interface ILibraryService
    {
        Task<IEnumerable<Library>> GetAllAsync();
        Task<Library> GetByIdAsync(int id);
        Task<Library> AddAsync(LibraryInputModel dto);
        Task<Library> UpdateAsync(int id, LibraryInputModel dto);
        Task<bool> RemoveAsync(int id);
        Task<IEnumerable<Library>> GetByNameAsync(string name);
    }
}
