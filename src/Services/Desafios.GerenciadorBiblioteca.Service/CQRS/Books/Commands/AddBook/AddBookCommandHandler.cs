﻿using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Commands.AddBook
{
    public class AddBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddBookCommand, CustomResponse<Book>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<Book>> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<AddBookCommandValidator, AddBookCommand>(request);

            var entity = _mapper.Map<Book>(request);

            entity = await _unitOfWork.Books.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ?
                new CustomResponse<Book>(entity, "Livro adicionado com Sucesso!") :
                throw new CustomException("Não foi possível adicionar o Livro. Tente novamente!", HttpStatusCode.InternalServerError);
        }
    }
}
