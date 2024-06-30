using Desafios.GerenciadorBiblioteca.Domain.Application.Services.Base;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;

namespace Desafios.GerenciadorBiblioteca.Domain.Application.Services
{
    public interface ILibraryService : IService<Library>
    {
        Task<IEnumerable<Library>> FindAsync(string name);
    }
}
