using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Repositories.Base;

namespace Desafios.GerenciadorBiblioteca.Domain.Infra.Repositories
{
    public interface ILibraryRepository : IGenericRepository<Library, int>
    {
    }
}
