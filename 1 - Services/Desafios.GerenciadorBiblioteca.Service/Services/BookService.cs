using Desafios.GerenciadorBiblioteca.Domain.Application.Entities.Books;
using Desafios.GerenciadorBiblioteca.Domain.Application.Entities.Loans;
using Desafios.GerenciadorBiblioteca.Domain.Application.Services;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Infra.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Services.Base;

namespace Desafios.GerenciadorBiblioteca.Service.Services
{
    public class BookSevice(IUnitOfWork unitOfWork) : ServiceBase, IBookService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var data = await _unitOfWork.Books.GetAllAsync();

            return data.Any() ? data : throw new Exception("Nenhum Livro encontrado.");
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(0, id);

            var data = await _unitOfWork.Books.GetByIdAsync(id);

            return ValidateReturnedDada(data, "Nenhum Livro encontrado.");
        }

        public async Task<IEnumerable<Book>> FindAsync(BookFilter filter)
        {
            var data = await GetAllAsync();

            FilterBooks(data, filter);

            return ValidateReturnedDada(data, "Nenhum Livro encontrado.");
        }

        public async Task<bool> AddAsync(Book entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _unitOfWork.Books.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível adicionar o Livro. Tente novamente!");
        }

        public async Task<bool> Update(Book entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var libraryRegistered = await GetByIdAsync(entity.Id);

            ArgumentNullException.ThrowIfNull(libraryRegistered);

            _unitOfWork.Books.Update(entity);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível alterar o Livro. Tente novamente!");
        }

        public async Task<bool> Remove(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(0, id);

            var libraryRegistered = await GetByIdAsync(id);

            ArgumentNullException.ThrowIfNull(libraryRegistered);

            _unitOfWork.Books.Remove(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível deletar o Livro. Tente novamente!");
        }

        private IEnumerable<Book> FilterBooks(IEnumerable<Book> books, BookFilter filter)
        {
            if (string.IsNullOrEmpty(filter.Title))
                books = books.Where(x => x.Title.Contains(filter.Title, StringComparison.CurrentCultureIgnoreCase));
            if (string.IsNullOrEmpty(filter.Author))
                books = books.Where(x => x.Author.Contains(filter.Author, StringComparison.CurrentCultureIgnoreCase));
            if (string.IsNullOrEmpty(filter.ISBN))
                books = books.Where(x => x.ISBN.Contains(filter.ISBN, StringComparison.CurrentCultureIgnoreCase));
            if (filter.Year == 0)
                books = books.Where(x => x.Year == filter.Year);

            return books;
        }
    }
}
