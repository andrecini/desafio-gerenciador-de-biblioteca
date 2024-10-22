﻿using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Commands.UpdateLibrary
{
    public class UpdateLibraryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateLibraryCommand, CustomResponse<Library>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<Library>> Handle(UpdateLibraryCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<UpdateLibraryCommandValidator, UpdateLibraryCommand>(request);

            var libraryRegistered = await _unitOfWork.Libraries.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhuma Biblioteca foi encontrada com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            libraryRegistered.Name = request.Name;
            libraryRegistered.CNPJ = request.CNPJ;
            libraryRegistered.Phone = request.Phone;

            _unitOfWork.Libraries.Update(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ?
                new CustomResponse<Library>(libraryRegistered, "Biblioteca alterada com sucesso!") :
                throw new CustomException("Não foi possível alterar a Biblioteca. Tente novamente!", HttpStatusCode.InternalServerError);
        }
    }
}
