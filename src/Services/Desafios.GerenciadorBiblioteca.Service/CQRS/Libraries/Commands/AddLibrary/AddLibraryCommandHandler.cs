using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Commands.AddLibrary
{
    public class AddLibraryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddLibraryCommand, CustomResponse<Library>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<Library>> Handle(AddLibraryCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<AddLibraryCommandValidator, AddLibraryCommand>(request);

            var entity = _mapper.Map<Library>(request);

            entity = await _unitOfWork.Libraries.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? 
                new CustomResponse<Library>(entity, "Biblioteca adicionada com sucesso!") :
                throw new CustomException("Não foi possível adicionar a Biblioteca. Tente novamente!", HttpStatusCode.InternalServerError);
        }
    }
}
