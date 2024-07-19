using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Repositories.Base;

namespace Desafios.GerenciadorBiblioteca.Domain.Repositories
{
    public interface ILibraryRepository : IGenericRepository<Library, int>
    {
        Task<IEnumerable<Library>> GetLibrariesByBook(int bookId);
        Task<IEnumerable<Library>> GetLibrariesByBookFiltered(int bookId, string name);
    }
}
