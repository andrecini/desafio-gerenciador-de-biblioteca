using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Models.Filters;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;

namespace Desafios.GerenciadorBiblioteca.Service.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> AddAsync(BookInputModel dto);
        Task<Book> UpdateAsync(int id, BookInputModel dto);
        Task<bool> RemoveAsync(int id);
        Task<IEnumerable<Book>> GetByFilterAsync(BookFilter filter);
        Task<IEnumerable<BookDetailsViewModel>> GetBooksDetailsByLibraryAsync(int libraryId);
        Task<IEnumerable<BookDetailsViewModel>> GetBooksDetailsFilteredAsync(int libraryId, BookFilter filter, int available);
    }
}
