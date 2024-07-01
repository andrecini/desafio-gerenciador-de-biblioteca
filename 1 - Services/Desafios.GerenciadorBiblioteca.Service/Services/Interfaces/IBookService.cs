using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Domain.Services.Base;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;

namespace Desafios.GerenciadorBiblioteca.Domain.Services
{
    public interface IBookService : IService<BookDTO, Book>
    {
        Task<IEnumerable<Book>> FindAsync(BookFilter filter);
    }
}
