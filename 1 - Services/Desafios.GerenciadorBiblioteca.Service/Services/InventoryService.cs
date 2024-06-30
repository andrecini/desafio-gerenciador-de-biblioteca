using Desafios.GerenciadorBiblioteca.Domain.Application.Entities.Inventory;
using Desafios.GerenciadorBiblioteca.Domain.Application.Enums;
using Desafios.GerenciadorBiblioteca.Domain.Application.Services;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Infra.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Services.Base;

namespace Desafios.GerenciadorBiblioteca.Service.Services
{
    public class InventoryService(IUnitOfWork unitOfWork) : ServiceBase, IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            var data = await _unitOfWork.Inventories.GetAllAsync();

            return data.Any() ? data : throw new Exception("Nenhum Livro encontrado.");
        }

        public async Task<Inventory> GetByIdAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(0, id);

            var data = await _unitOfWork.Inventories.GetByIdAsync(id);

            return ValidateReturnedDada(data, "Nenhum Livro encontrado.");
        }

        public async Task<IEnumerable<Inventory>> FindAsync(InventoryFilter filter)
        {
            var data = await GetAllAsync();

            FilterInventories(data, filter);

            return ValidateReturnedDada(data, "Nenhum Livro encontrado.");
        }

        public async Task<bool> AddAsync(Inventory entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _unitOfWork.Inventories.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível adicionar o Livro. Tente novamente!");
        }

        public async Task<bool> Update(Inventory entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var libraryRegistered = await GetByIdAsync(entity.Id);

            ArgumentNullException.ThrowIfNull(libraryRegistered);

            _unitOfWork.Inventories.Update(entity);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível alterar o Livro. Tente novamente!");
        }

        public async Task<bool> Remove(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(0, id);

            var libraryRegistered = await GetByIdAsync(id);

            ArgumentNullException.ThrowIfNull(libraryRegistered);

            _unitOfWork.Inventories.Remove(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível deletar o Livro. Tente novamente!");
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
