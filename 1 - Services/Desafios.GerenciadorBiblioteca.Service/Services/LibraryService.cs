using Desafios.GerenciadorBiblioteca.Domain.Application.Services;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Infra.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Services.Base;

namespace Desafios.GerenciadorBiblioteca.Service.Services
{
    public class LibrarySevice(IUnitOfWork unitOfWork) : ServiceBase, ILibraryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Library>> GetAllAsync()
        {
            var data = await _unitOfWork.Libraries.GetAllAsync();

            return data.Any() ? data : throw new Exception("Nenhuma Biblioteca encontrada.");
        }

        public async Task<Library> GetByIdAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(0, id);

            var data = await _unitOfWork.Libraries.GetByIdAsync(id);

            return ValidateReturnedDada(data, "Nenhuma Biblioteca encontrada.");
        }

        public async Task<IEnumerable<Library>> FindAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);

            var data = await _unitOfWork.Libraries.FindAsync(x => x.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));

            return ValidateReturnedDada(data, "Nenhuma Biblioteca encontrada.");
        }

        public async Task<bool> AddAsync(Library entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _unitOfWork.Libraries.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível adicionar a Biblioteca. Tente novamente!");
        }

        public async Task<bool> Update(Library entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var libraryRegistered = await GetByIdAsync(entity.Id);

            ArgumentNullException.ThrowIfNull(libraryRegistered);

            _unitOfWork.Libraries.Update(entity);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível alterar a Biblioteca. Tente novamente!");
        }

        public async Task<bool> Remove(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(0, id);

            var libraryRegistered = await GetByIdAsync(id);

            ArgumentNullException.ThrowIfNull(libraryRegistered);

            _unitOfWork.Libraries.Remove(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível deletar a Biblioteca. Tente novamente!");
        }
    }
}
