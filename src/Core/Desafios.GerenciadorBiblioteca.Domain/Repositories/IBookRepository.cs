using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryResults;
using Desafios.GerenciadorBiblioteca.Domain.Repositories.Base;

namespace Desafios.GerenciadorBiblioteca.Domain.Repositories
{
    public interface IBookRepository : IGenericRepository<Book, int>
    {
        Task<IEnumerable<BookDetailsQueryResult>> GetBooksDetailsByLibraryAsync(BookDetailsQueryRequest request);

        Task<IEnumerable<BookDetailsQueryResult>> GetBooksDetailsByFilterAsync(BookDetailsFilteredQueryRequest request);
    }
}
