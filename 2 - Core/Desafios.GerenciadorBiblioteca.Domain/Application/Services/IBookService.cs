using Desafios.GerenciadorBiblioteca.Domain.Application.Entities.Books;
using Desafios.GerenciadorBiblioteca.Domain.Application.Services.Base;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;

namespace Desafios.GerenciadorBiblioteca.Domain.Application.Services
{
    public interface IBookService : IService<Book>
    {
        Task<IEnumerable<Book>> FindAsync(BookFilter filter);
    }
}
