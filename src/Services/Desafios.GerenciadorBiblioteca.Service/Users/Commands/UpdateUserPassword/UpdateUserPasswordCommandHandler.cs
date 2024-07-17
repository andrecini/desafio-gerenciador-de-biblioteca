﻿using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Security.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Desafios.GerenciadorBiblioteca.Service.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, UserViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICipherService _cipher;

        public UpdateUserPasswordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICipherService cipher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cipher = cipher;
        }

        public async Task<UserViewModel> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity< UpdateUserPasswordCommandValidator, UpdateUserPasswordCommand>(request);

            var userRegistered = await _unitOfWork.Users.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Usuário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _cipher.ValidatePasswordPolicy(request.NewPassword);

            userRegistered.Password = _cipher.Encrypt(request.NewPassword);

            _unitOfWork.Users.Update(userRegistered);
            var result = await _unitOfWork.SaveAsync();

            var viewModel = _mapper.Map<UserViewModel>(userRegistered);

            return result > 0 ? viewModel : throw new CustomException(
                "Não foi possível alterar a senha do Usuário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}