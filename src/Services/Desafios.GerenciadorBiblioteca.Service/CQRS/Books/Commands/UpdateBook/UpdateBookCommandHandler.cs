using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Validators;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateBookCommand, CustomResponse<Book>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CustomResponse<Book>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<UpdateBookCommandValidator, UpdateBookCommand>(request);

            var libraryRegistered = await _unitOfWork.Books.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Livro foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound); //TODO: Criar Lógica de verificação de ID

            libraryRegistered.Title = request.Title;
            libraryRegistered.Author = request.Author;
            libraryRegistered.ISBN = request.ISBN;
            libraryRegistered.Year = request.Year;

            _unitOfWork.Books.Update(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ?
               new CustomResponse<Book>(libraryRegistered, "Livro alterado com Sucesso!") :
               throw new CustomException("Não foi possível alterar o Livro. Tente novamente!", HttpStatusCode.InternalServerError);
        }
    }
}
