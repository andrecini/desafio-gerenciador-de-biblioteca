using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Base;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Security.Interfaces;
using Desafios.GerenciadorBiblioteca.Service.Services.Base;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Desafios.GerenciadorBiblioteca.Service.Validators;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Services
{
    public class UserService(IUnitOfWork unitOfWork, IMapper mapper, ICipherService cipher) : BaseService, IUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICipherService _cipher = cipher;

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var data = await _unitOfWork.Users.GetAllAsync();

            var viewModel = _mapper.Map<IEnumerable<UserViewModel>>(data);

            return viewModel;
        }

        public async Task<UserViewModel> GetByIdAsync(int id)
        {
            CustomException.ThrowIfLessThanOne(id, "Id");

            var data = await _unitOfWork.Users.GetByIdAsync(id);

            var viewModel = _mapper.Map<UserViewModel>(data);

            return viewModel;
        }

        public async Task<IEnumerable<UserViewModel>> GetByNameAsync(string name)
        {
            var users = await _unitOfWork.Users.GetAllAsync();

            if (!string.IsNullOrEmpty(name))
                users = users.Where(x => x.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase));

            var viewModel = _mapper.Map<IEnumerable<UserViewModel>>(users);

            return viewModel;
        }

        public async Task<UserViewModel> AddAsync(UserRegisterInputModel dto)
        {
            CustomException.ThrowIfNull(dto, "Usuário");

            ValidateEntity<UserRegisterValidator, UserRegisterInputModel>(dto);

            var existingUseremail = await GetUserByEmailAsync(dto.Email);

            if (existingUseremail != null)
                throw new CustomException("Email já cadastrado!", HttpStatusCode.UnprocessableEntity);

            _cipher.ValidatePasswordPolicy(dto.Password);

            var entity = _mapper.Map<User>(dto);

            entity.Password = _cipher.Encrypt(dto.Password);
            entity.Role = Roles.Common;

            entity = await _unitOfWork.Users.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            var viewModel = _mapper.Map<UserViewModel>(entity);

            return result > 0 ? viewModel : throw new CustomException(
                "Não foi possível adicionar o Usuário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<UserViewModel> UpdateAsync(int id, UserUpdateInputModel dto)
        {
            CustomException.ThrowIfNull(dto, "Usuário");

            ValidateEntity<UserUpdateValidator, UserUpdateInputModel>(dto);

            var user = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Usuário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            var userRegistered = await _unitOfWork.Users.GetByIdAsync(id);

            userRegistered.Name = dto.Name;
            userRegistered.Email = dto.Email;
            userRegistered.Phone = dto.Phone;

            _unitOfWork.Users.Update(userRegistered);
            var result = await _unitOfWork.SaveAsync();

            var viewModel = _mapper.Map<UserViewModel>(userRegistered);

            return result > 0 ? viewModel : throw new CustomException(
                 "Não foi possível alterar o Usuário. Tente novamente!",
                 HttpStatusCode.InternalServerError);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            CustomException.ThrowIfLessThanOne(id, "Id");

            var userRegistered = await _unitOfWork.Users.GetByIdAsync(id) ??
                throw new CustomException("Nenhum Usuário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _unitOfWork.Users.Remove(userRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? true : throw new CustomException(
                "Não foi possível deletar o Usuário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }

        public async Task<UserViewModel> LoginAsync(UserLoginInputModel dto)
        {
            CustomException.ThrowIfNull(dto, "Usuário");

            if (string.IsNullOrEmpty(dto.Email))
                throw new CustomException("O Email é obrigatório!", HttpStatusCode.UnprocessableEntity);
            if (string.IsNullOrEmpty(dto.Password))
                throw new CustomException("A Senha é obrigatória!", HttpStatusCode.UnprocessableEntity);

            var user = await GetUserByEmailAsync(dto.Email) ?? throw new CustomException("Email e/ou Senha Inválidos!", HttpStatusCode.Unauthorized);

            var isValid = _cipher.ValidatePassword(dto.Password, user.Password);

            var viewModel = _mapper.Map<UserViewModel>(user);

            return viewModel;
        }

        public async Task<UserViewModel> UpdatePasswordAsync(int id, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
                throw new CustomException("A Nova Senha é obrigatória!", HttpStatusCode.UnprocessableEntity);

            var user = await GetByIdAsync(id) ??
                throw new CustomException("Nenhum Usuário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            var userRegistered = await _unitOfWork.Users.GetByIdAsync(id);

            _cipher.ValidatePasswordPolicy(newPassword);

            userRegistered.Password = _cipher.Encrypt(newPassword);

            _unitOfWork.Users.Update(userRegistered);
            var result = await _unitOfWork.SaveAsync();

            var viewModel = _mapper.Map<UserViewModel>(userRegistered);

            return result > 0 ? viewModel : throw new CustomException(
                 "Não foi possível alterar a senha do Usuário. Tente novamente!",
                 HttpStatusCode.InternalServerError);
        }

        private async Task<User> GetUserByEmailAsync(string email)
        {
            var users = await _unitOfWork.Users.FindAsync(x => x.Email == email);

            return users.FirstOrDefault();
        }
    }
}
