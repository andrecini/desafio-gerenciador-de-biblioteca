﻿using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Commands.RemoveBook
{
    public class RemoveBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveBookCommand, CustomResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<bool>> Handle(RemoveBookCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<RemoveBookCommandValidator, RemoveBookCommand>(request);

            var libraryRegistered = await _unitOfWork.Books.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Livro foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound); //TODO: Criar Lógica de verificação de ID

            _unitOfWork.Books.Remove(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? 
                new CustomResponse<bool>(true, "Livro removido com Sucesso!") : 
                throw new CustomException("Não foi possível deletar o Livro. Tente novamente!", HttpStatusCode.InternalServerError);
        }
    }
}
