using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using Desafios.GerenciadorBiblioteca.Service.Validators;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            CustomException.ThrowIfNull(request, "Livro");

            ValidatorHelper.ValidateEntity<UpdateBookCommandValidator, UpdateBookCommand>(request);

            var libraryRegistered = await _unitOfWork.Books.GetByIdAsync(request.Id) ??
                throw new CustomException("Nenhum Livro foi encontrado com essas informações. Tente novamente!", HttpStatusCode.NotFound);

            libraryRegistered.Title = request.Title;
            libraryRegistered.Author = request.Author;
            libraryRegistered.ISBN = request.ISBN;
            libraryRegistered.Year = request.Year;

            _unitOfWork.Books.Update(libraryRegistered);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? libraryRegistered : throw new CustomException(
                "Não foi possível alterar o Livro. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}
