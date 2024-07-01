using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Services.Base;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;

namespace Desafios.GerenciadorBiblioteca.Domain.Services
{
    public interface ILibraryService : IService<LibraryDTO, Library>
    {
        Task<IEnumerable<Library>> FindAsync(string name);
    }
}
