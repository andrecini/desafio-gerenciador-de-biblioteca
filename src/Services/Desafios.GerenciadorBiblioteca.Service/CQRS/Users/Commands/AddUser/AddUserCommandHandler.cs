using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Security.Interfaces;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.AddUser
{
    public class AddUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICipherService cipher) : IRequestHandler<AddUserCommand, CustomResponse<UserViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICipherService _cipher = cipher;

        public async Task<CustomResponse<UserViewModel>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<AddUserCommandValidator, AddUserCommand>(request);

            var existingUseremail = await _unitOfWork.Users.FindAsync(x => x.Email == request.Email);

            if (existingUseremail.Any())
                throw new CustomException("Email já cadastrado!", HttpStatusCode.UnprocessableEntity);

            _cipher.ValidatePasswordPolicy(request.Password);

            var entity = _mapper.Map<User>(request);
            entity.Password = _cipher.Encrypt(request.Password);
            entity.Role = Roles.Common;

            entity = await _unitOfWork.Users.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            var viewModel = _mapper.Map<UserViewModel>(entity);

            return result > 0 ? 
                new CustomResponse<UserViewModel>(viewModel, "Usuário adicionado com sucesso!") : 
                throw new CustomException("Não foi possível adicionar o Usuário. Tente novamente!", HttpStatusCode.InternalServerError);
        }
    }
}
