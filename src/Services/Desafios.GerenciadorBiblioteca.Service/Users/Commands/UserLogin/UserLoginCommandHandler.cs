using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Security.Interfaces;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Users.Commands.UserLogin
{
    public class UserLoginCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICipherService cipher) : IRequestHandler<UserLoginCommand, UserViewModel>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICipherService _cipher = cipher;

        public async Task<UserViewModel> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<UserLoginCommandValidator, UserLoginCommand>(request);

            var user = await _unitOfWork.Users.FindAsync(x => x.Email == request.Email);
            var userRegistered = user.FirstOrDefault() ?? throw new CustomException("Email e/ou Senha Inválidos!", HttpStatusCode.Unauthorized);

            var passwordEncrypted = _cipher.Encrypt(request.Password);
            var isValid = _cipher.ValidatePassword(passwordEncrypted, userRegistered.Password);

            if (!isValid)
                throw new CustomException("Email e/ou Senha Inválidos!", HttpStatusCode.Unauthorized);

            var viewModel = _mapper.Map<UserViewModel>(userRegistered);

            return viewModel;
        }
    }
}