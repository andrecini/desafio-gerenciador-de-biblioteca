using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.Libraries.Queries.GetLibraryById
{
    public class GetLibraryByIdQueryHandler : IRequestHandler<GetLibraryByIdQuery, Library>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLibraryByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Library> Handle(GetLibraryByIdQuery request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            CustomException.ThrowIfLessThanOne(id, "Id");

            var data = await _unitOfWork.Libraries.GetByIdAsync(id);

            return data;
        }
    }
}
