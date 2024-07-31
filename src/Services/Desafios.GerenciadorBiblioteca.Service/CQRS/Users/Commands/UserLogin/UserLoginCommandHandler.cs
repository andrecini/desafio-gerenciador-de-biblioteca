using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Security;
using Desafios.GerenciadorBiblioteca.Service.Security.Interfaces;
using MediatR;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UserLogin
{
    public class UserLoginCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICipherService cipher, ITokenService tokenService) : 
        IRequestHandler<UserLoginCommand, CustomResponse<TokenViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICipherService _cipher = cipher;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<CustomResponse<TokenViewModel>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<UserLoginCommandValidator, UserLoginCommand>(request);

            var user = await _unitOfWork.Users.FindAsync(x => x.Email == request.Email);
            var userRegistered = user.FirstOrDefault() ?? throw new CustomException("Email e/ou Senha Inválidos!", HttpStatusCode.Unauthorized);

            var passwordEncrypted = _cipher.Encrypt(request.Password);
            var isValid = _cipher.ValidatePassword(passwordEncrypted, userRegistered.Password);

            if (!isValid)
                throw new CustomException("Email e/ou Senha Inválidos!", HttpStatusCode.Unauthorized);

            var token = _tokenService.GenerateJwtToken(userRegistered.Id, userRegistered.Name, userRegistered.Role);

            return new CustomResponse<TokenViewModel>(token, "Login realizado com sucesso!");
        }
    }
}