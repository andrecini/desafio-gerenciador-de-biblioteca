using Desafios.GerenciadorBiblioteca.Domain.Application.Services;
using Desafios.GerenciadorBiblioteca.Domain.Infra.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Infra.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Services.Base;

namespace Desafios.GerenciadorBiblioteca.Service.Services
{
    public class UserSevice(IUnitOfWork unitOfWork) : ServiceBase, IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var data = await _unitOfWork.Users.GetAllAsync();

            return data.Any() ? data : throw new Exception("Nenhum Usuário encontrado.");
        }

        public async Task<User> GetByIdAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(0, id);

            var data = await _unitOfWork.Users.GetByIdAsync(id);

            return ValidateReturnedDada(data, "Nenhum Usuário encontrado.");
        }

        public async Task<IEnumerable<User>> FindAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(name);

            var data = await _unitOfWork.Users.FindAsync(x => x.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));

            return ValidateReturnedDada(data, "Nenhuma Biblioteca encontrada.");
        }

        public async Task<bool> AddAsync(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            await _unitOfWork.Users.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível adicionar o Usuário. Tente novamente!");
        }

        public async Task<bool> Update(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var libraryRegistered = await GetByIdAsync(entity.Id);

            ArgumentNullException.ThrowIfNull(libraryRegistered);

            _unitOfWork.Users.Update(entity);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível atualizar o Usuário. Tente novamente!");
        }

        public async Task<bool> Remove(int id)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(0, id);

            var libraryRegistered = await GetByIdAsync(id);

            ArgumentNullException.ThrowIfNull(libraryRegistered);

            _unitOfWork.Users.Remove(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return ValidateResult(result, "Não foi possível deletar o Usuário. Tente novamente!");
        }
    }
}
