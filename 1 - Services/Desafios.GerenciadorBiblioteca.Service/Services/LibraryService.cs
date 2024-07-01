﻿using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Services
{
    public class LibrarySevice(IUnitOfWork unitOfWork, IMapper mapper) : ILibraryService
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
            CustomException.ThrowIfLessThan(0, "Id");

            var data = await _unitOfWork.Libraries.GetByIdAsync(id);

            return data;
        }

        public async Task<IEnumerable<Library>> FindAsync(string name)
        {
            if (!string.IsNullOrEmpty(name))
                return await _unitOfWork.Libraries.FindAsync(x => x.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));

            return await GetAllAsync();
        }

        public async Task<bool> AddAsync(LibraryInpuDTO dto)
        {
            CustomException.ThrowIfNull(dto, "Biblioteca");

            var entity = _mapper.Map<Library>(dto);

            await _unitOfWork.Libraries.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível adicionar a Biblioteca. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<bool> Update(int id, LibraryInpuDTO dto)
        {
            CustomException.ThrowIfNull(dto, "Biblioteca");

            var libraryRegistered = await GetByIdAsync(id) ??
               throw new CustomException("Nenhuma Biblioteca foi encontrada com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            var entity = _mapper.Map<Library>(dto);

            _unitOfWork.Libraries.Update(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível alterar a Biblioteca. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<bool> Remove(int id)
        {
            CustomException.ThrowIfLessThan(0, "Id");

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
