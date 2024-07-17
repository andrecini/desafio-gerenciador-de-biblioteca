﻿using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateUserCommand, UserViewModel>
    {
        public async Task<UserViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<UpdateUserCommandValidator, UpdateUserCommand>(request);

            var userRegistered = await unitOfWork.Users.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Usuário foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            userRegistered.Name = request.Name;
            userRegistered.Email = request.Email;
            userRegistered.Phone = request.Phone;

            unitOfWork.Users.Update(userRegistered);
            var result = await unitOfWork.SaveAsync();

            var viewModel = mapper.Map<UserViewModel>(userRegistered);

            return result > 0 ? viewModel : throw new CustomException(
                "Não foi possível alterar o Usuário. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}