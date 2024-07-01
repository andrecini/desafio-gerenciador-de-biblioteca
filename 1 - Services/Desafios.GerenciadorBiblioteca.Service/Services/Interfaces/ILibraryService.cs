using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces.Base;

namespace Desafios.GerenciadorBiblioteca.Service.Services.Interfaces
{
    public interface ILibraryService : IService<LibraryInpuDTO, Library>
    {
        Task<IEnumerable<Library>> FindAsync(string name);
    }
}
