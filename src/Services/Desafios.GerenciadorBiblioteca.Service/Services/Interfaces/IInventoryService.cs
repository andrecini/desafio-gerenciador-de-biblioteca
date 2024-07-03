﻿using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces.Base;

namespace Desafios.GerenciadorBiblioteca.Service.Services.Interfaces
{
    public interface IInventoryService : IService<InventoryDTO, Inventory>
    {
        Task<IEnumerable<Inventory>> GetByFilterAsync(InventoryFilter filter);
        Task<IEnumerable<Inventory>> GetByLibraryAsync(int libraryId);
        Task<Inventory> UpdateStatusAsync(int id, bool available);
    }
}