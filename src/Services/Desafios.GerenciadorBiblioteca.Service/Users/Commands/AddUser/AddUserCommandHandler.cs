using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Security.Interfaces;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Users.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICipherService _cipher;

        public AddUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICipherService cipher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cipher = cipher;
        }

        public async Task<UserViewModel> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            CustomException.ThrowIfNull(request, "Usuário");

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

            return result > 0 ? viewModel : throw new CustomException(
                "Não foi possível adicionar o Usuário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}
