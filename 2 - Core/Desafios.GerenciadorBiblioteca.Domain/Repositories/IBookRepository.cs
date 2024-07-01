using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Repositories.Base;

namespace Desafios.GerenciadorBiblioteca.Domain.Repositories
{
    public interface IBookRepository : IGenericRepository<Book, int>
    {
    }
}
