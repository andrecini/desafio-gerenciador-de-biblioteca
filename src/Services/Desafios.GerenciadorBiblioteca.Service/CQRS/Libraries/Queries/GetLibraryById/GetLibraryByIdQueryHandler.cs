using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Libraries.Queries.GetLibraryById
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
            ValidatorHelper.ValidateEntity<GetLibraryByIdQueryValidator, GetLibraryByIdQuery>(request);

            var data = await _unitOfWork.Libraries.GetByIdAsync(request.Id);

            return data;
        }
    }
}
