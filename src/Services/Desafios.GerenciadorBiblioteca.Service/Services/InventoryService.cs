﻿using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services.Base;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Desafios.GerenciadorBiblioteca.Service.Validators;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Services
{
    public class InventoryService(IUnitOfWork unitOfWork, IMapper mapper) : BaseService, IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            var data = await _unitOfWork.Inventories.GetAllAsync();

            return data;
        }

        public async Task<Inventory> GetByIdAsync(int id)
        {
            CustomException.ThrowIfLessThan(0, "Id");

            var data = await _unitOfWork.Inventories.GetByIdAsync(id);

            return data;
        }

        public async Task<IEnumerable<Inventory>> GetByFilterAsync(InventoryFilter filter)
        {
            var data = await GetAllAsync();

            data = FilterInventories(data, filter);

            return data;
        }

        public async Task<IEnumerable<Inventory>> GetByLibraryAsync(int libraryId)
        {
            var data = await _unitOfWork.Inventories.FindAsync(x => x.LibraryId == libraryId);

            return data;
        }

        public async Task<Inventory> AddAsync(InventoryDTO dto)
        {
            CustomException.ThrowIfNull(dto, "Inventário");

            ValidateEntity<InventoryValidator, InventoryDTO>(dto);

            var entity = _mapper.Map<Inventory>(dto);
            entity.Available = true;

            entity = await _unitOfWork.Inventories.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? entity : throw new CustomException(
                "Não foi possível adicionar o Inventário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<Inventory> UpdateAsync(int id, InventoryDTO dto)
        {
            CustomException.ThrowIfNull(dto, "Inventário");

            ValidateEntity<InventoryValidator, InventoryDTO>(dto);

            var InventoryRegistered = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Inventário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            InventoryRegistered.LibraryId = dto.LibraryId;
            InventoryRegistered.BookId = dto.BookId;
            InventoryRegistered.Available = true;

            _unitOfWork.Inventories.Update(InventoryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? InventoryRegistered : throw new CustomException(
                "Não foi possível alterar o Inventário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<Inventory> UpdateStatusAsync(int id, bool available)
        {
            CustomException.ThrowIfLessThan(1, "Id");

            var InventoryRegistered = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Inventário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            InventoryRegistered.Available = available;

            _unitOfWork.Inventories.Update(InventoryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? InventoryRegistered : throw new CustomException(
                "Não foi possível alterar o Status do Inventário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            CustomException.ThrowIfLessThan(1, "Id");

            var InventoryRegistered = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Inventário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _unitOfWork.Inventories.Remove(InventoryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                 "Não foi possível deletar o Inventário. Tente novamente!",
                 HttpStatusCode.InternalServerError);
        }

        private IEnumerable<Inventory> FilterInventories(IEnumerable<Inventory> inventories, InventoryFilter filter)
        {
            if (filter.LibraryId >= 0)
                inventories = inventories.Where(x => x.LibraryId == filter.LibraryId);
            if (filter.BookId >= 0)
                inventories = inventories.Where(x => x.BookId == filter.BookId);
            if (filter.Status == InventoryStatus.Available)
                inventories = inventories.Where(x => x.Available == true);
            if (filter.Status == InventoryStatus.Unavailable)
                inventories = inventories.Where(x => x.Available == false);

            return inventories;
        }
    }
}