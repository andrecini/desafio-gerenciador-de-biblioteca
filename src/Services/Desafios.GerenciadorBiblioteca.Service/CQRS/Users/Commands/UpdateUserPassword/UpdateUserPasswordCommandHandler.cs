using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
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

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICipherService cipher) : IRequestHandler<UpdateUserPasswordCommand, CustomResponse<UserViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly ICipherService _cipher = cipher;

        public async Task<CustomResponse<UserViewModel>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<UpdateUserPasswordCommandValidator, UpdateUserPasswordCommand>(request);

            var userRegistered = await _unitOfWork.Users.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Usuário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            _cipher.ValidatePasswordPolicy(request.NewPassword);

            userRegistered.Password = _cipher.Encrypt(request.NewPassword);

            _unitOfWork.Users.Update(userRegistered);
            var result = await _unitOfWork.SaveAsync();

            var viewModel = _mapper.Map<UserViewModel>(userRegistered);

            return result > 0 ?
                 new CustomResponse<UserViewModel>(viewModel, "Usuário alterado com sucesso!") :
                 throw new CustomException("Não foi possível alterado o Usuário. Tente novamente!", HttpStatusCode.InternalServerError);
        }
    }
}