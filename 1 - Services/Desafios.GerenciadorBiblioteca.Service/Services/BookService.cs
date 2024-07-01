using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Services
{
    public class BookSevice(IUnitOfWork unitOfWork, IMapper mapper) : IBookService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var data = await _unitOfWork.Books.GetAllAsync();

            return data;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            CustomException.ThrowIfLessThan(0, "Id");

            var data = await _unitOfWork.Books.GetByIdAsync(id);

            return data;
        }

        public async Task<IEnumerable<Book>> FindAsync(BookFilter filter)
        {
            var data = await GetAllAsync();

            data = FilterBooks(data, filter);

            return data;
        }

        public async Task<bool> AddAsync(BookInputDTO dto)
        {
            CustomException.ThrowIfNull(dto, "Livro");

            var entity = _mapper.Map<Book>(dto);

            await _unitOfWork.Books.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível adicionar o Livro. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<bool> Update(int id, BookInputDTO dto)
        {
            CustomException.ThrowIfNull(dto, "Livro");

            var libraryRegistered = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Livro foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            var entity = _mapper.Map<Book>(dto);

            _unitOfWork.Books.Update(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível alterar o Livro. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<bool> Remove(int id)
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
