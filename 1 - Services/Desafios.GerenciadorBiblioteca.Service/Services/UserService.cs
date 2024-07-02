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
    public class UserService(IUnitOfWork unitOfWork, IMapper mapper) : BaseService, IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var data = await _unitOfWork.Users.GetAllAsync();

            return data;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            CustomException.ThrowIfLessThan(0, "Id");

            var data = await _unitOfWork.Users.GetByIdAsync(id);

            return data;
        }

        public async Task<IEnumerable<User>> FindAsync(string name)
        {
            if (!string.IsNullOrEmpty(name))
                return await _unitOfWork.Users.FindAsync(x => x.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));

            return await GetAllAsync();
        }

        public async Task<User> AddAsync(UserDTO dto)
        {
            CustomException.ThrowIfNull(dto, "Usuário");

            ValidateEntity<UserValidator, UserDTO>(dto);

            var entity = _mapper.Map<User>(dto);

            entity = await _unitOfWork.Users.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? entity : throw new CustomException(
                "Não foi possível adicionar o Usuário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<User> Update(int id, UserDTO dto)
        {
            CustomException.ThrowIfNull(dto, "Usuário");

            ValidateEntity<UserValidator, UserDTO>(dto);

            var userRegistered = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Usuário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            var entity = _mapper.Map<User>(dto);
            entity.Id = id;

            _unitOfWork.Users.Update(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? entity : throw new CustomException(
                 "Não foi possível alterar o Usuário. Tente novamente!",
                 HttpStatusCode.InternalServerError);
        }

        public async Task<bool> Remove(int id)
        {
            CustomException.ThrowIfLessThan(0, "Id");

            var userRegistered = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Livro foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _unitOfWork.Users.Remove(userRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível deletar o Usuário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}
