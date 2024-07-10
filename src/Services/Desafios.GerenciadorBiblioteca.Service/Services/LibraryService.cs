using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services.Base;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Desafios.GerenciadorBiblioteca.Service.Validators;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Services
{
    public class LibraryService(IUnitOfWork unitOfWork, IMapper mapper) : BaseService, ILibraryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<Library>> GetAllAsync()
        {
            var data = await _unitOfWork.Libraries.GetAllAsync();

            return data;
        }

        public async Task<Library> GetByIdAsync(int id)
        {
            CustomException.ThrowIfLessThanOne(id, "Id");

            var data = await _unitOfWork.Libraries.GetByIdAsync(id);

            return data;
        }

        public async Task<IEnumerable<Library>> GetByNameAsync(string name)
        {           
            var libraries = await _unitOfWork.Libraries.GetAllAsync();

            if (!string.IsNullOrEmpty(name))
                return libraries.Where(x => x.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));

            return libraries;
        }

        public async Task<Library> AddAsync(LibraryDTO dto)
        {
            CustomException.ThrowIfNull(dto, "Biblioteca");

            ValidateEntity<LibraryValidator, LibraryDTO>(dto);

            var entity = _mapper.Map<Library>(dto);

            entity = await _unitOfWork.Libraries.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? entity : throw new CustomException(
                "Não foi possível adicionar a Biblioteca. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<Library> UpdateAsync(int id, LibraryDTO dto)
        {
            CustomException.ThrowIfNull(dto, "Biblioteca");

            ValidateEntity<LibraryValidator, LibraryDTO>(dto);

            var libraryRegistered = await GetByIdAsync(id) ??
               throw new CustomException("Nenhuma Biblioteca foi encontrada com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            libraryRegistered.Name = dto.Name;
            libraryRegistered.CNPJ = dto.CNPJ;
            libraryRegistered.Phone = dto.Phone;

            _unitOfWork.Libraries.Update(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? libraryRegistered : throw new CustomException(
                "Não foi possível alterar a Biblioteca. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            CustomException.ThrowIfLessThanOne(id, "Id");

            var libraryRegistered = await GetByIdAsync(id) ??
               throw new CustomException("Nenhuma Biblioteca foi encontrada com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _unitOfWork.Libraries.Remove(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                 "Não foi possível deletar a Biblioteca. Tente novamente!",
                 HttpStatusCode.InternalServerError);
        }
    }
}
