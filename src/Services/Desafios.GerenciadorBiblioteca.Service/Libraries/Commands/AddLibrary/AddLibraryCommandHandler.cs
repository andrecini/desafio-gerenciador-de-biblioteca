using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;
using System.Net;

namespace Desafios.GerenciadorBiblioteca.Service.Libraries.Commands.AddLibrary
{
    public class AddLibraryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddLibraryCommand, Library>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Library> Handle(AddLibraryCommand request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<AddLibraryCommandValidator, AddLibraryCommand>(request);

            var entity = _mapper.Map<Library>(request);

            entity = await _unitOfWork.Libraries.AddAsync(entity);
            var result = await _unitOfWork.SaveAsync();

            return result > 0 ? entity : throw new CustomException(
                "Não foi possível adicionar a Biblioteca. Tente novamente!",
                HttpStatusCode.InternalServerError);
        }
    }
}
