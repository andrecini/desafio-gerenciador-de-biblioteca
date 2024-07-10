using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Services.Base;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Desafios.GerenciadorBiblioteca.Service.Validators;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Services
{
    public class BookService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IInventoryService inventoryService) : BaseService, IBookService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IInventoryService _inventoryService;

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var data = await _unitOfWork.Books.GetAllAsync();

            return data;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            CustomException.ThrowIfLessThan(id, "Id");

            var data = await _unitOfWork.Books.GetByIdAsync(id);

            return data;
        }

        public async Task<IEnumerable<Book>> GetByFilterAsync(BookFilter filter)
        {
            var data = await GetAllAsync();

            data = FilterBooks(data, filter);

            return data;
        }

        public async Task<Book> AddAsync(BookDTO dto)
        {
            CustomException.ThrowIfNull(dto, "Livro");

            ValidateEntity<BookValidator, BookDTO>(dto);

            var entity = _mapper.Map<Book>(dto);

            entity = await _unitOfWork.Books.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? entity : throw new CustomException(
                "Não foi possível adicionar o Livro. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<Book> UpdateAsync(int id, BookDTO dto)
        {
            CustomException.ThrowIfNull(dto, "Livro");

            ValidateEntity<BookValidator, BookDTO>(dto);

            var libraryRegistered = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Livro foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            libraryRegistered.Title = dto.Title;
            libraryRegistered.Author = dto.Author;
            libraryRegistered.ISBN = dto.ISBN;
            libraryRegistered.Year = dto.Year;

            _unitOfWork.Books.Update(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? libraryRegistered : throw new CustomException(
                "Não foi possível alterar o Livro. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            CustomException.ThrowIfLessThan(0, "Id");

            var libraryRegistered = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Livro foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _unitOfWork.Books.Remove(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível deletar o Livro. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<IEnumerable<BookDetailsDTO>> GetBooksDetailsByLibraryAsync(int libraryId)
        {
            var inventories = await inventoryService.GetByLibraryAsync(libraryId);

            var bookIds = inventories.Select(inv => inv.BookId).ToHashSet();
            var allBooks = await GetAllAsync();
            var books = allBooks.Where(book => bookIds.Contains(book.Id)).ToList();

            var booksViewModels = books.Select(book =>
            {
                var inventory = inventories.FirstOrDefault(inv => inv.BookId == book.Id);
                bool available = inventory != null && inventory.Available;
                return new BookDetailsDTO(book, inventory.Id, available);
            });

            return booksViewModels;
        }

        public async Task<IEnumerable<BookDetailsDTO>> GetBooksDetailsFilteredAsync(int libraryId, BookFilter filter, int available)
        {
            var inventories = await inventoryService.GetByLibraryAsync(libraryId);

            if (available != 0)
            {
                bool isAvailable = available == 1;
                inventories = inventories.Where(x => x.Available == isAvailable).ToList();
            }

            var bookIds = inventories.Select(inv => inv.BookId).ToHashSet();
            var allBooks = await GetByFilterAsync(filter);
            var filteredBooks = allBooks.Where(book => bookIds.Contains(book.Id)).ToList();

            var booksViewModels = filteredBooks.Select(book =>
            {
                var inventory = inventories.FirstOrDefault(inv => inv.BookId == book.Id);
                bool available = inventory != null && inventory.Available;
                return new BookDetailsDTO(book, inventory.Id, available);
            });

            return booksViewModels;
        }
    
        private IEnumerable<Book> FilterBooks(IEnumerable<Book> books, BookFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Title))
                books = books.Where(x => x.Title.Contains(filter.Title, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrEmpty(filter.Author))
                books = books.Where(x => x.Author.Contains(filter.Author, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrEmpty(filter.ISBN))
                books = books.Where(x => x.ISBN.Contains(filter.ISBN, StringComparison.CurrentCultureIgnoreCase));
            if (filter.Year > 0)
                books = books.Where(x => x.Year == filter.Year);

            return books;
        }
    }
}
