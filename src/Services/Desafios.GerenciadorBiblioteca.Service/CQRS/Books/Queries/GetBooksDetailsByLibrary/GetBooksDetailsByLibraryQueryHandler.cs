using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBooksDetailsByLibrary
{
    public class GetBooksDetailsByLibraryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetBooksDetailsByLibraryQuery, CustomResponse<IEnumerable<BookDetailsViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<IEnumerable<BookDetailsViewModel>>> Handle(GetBooksDetailsByLibraryQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetBooksDetailsByLibraryQueryValidator, GetBooksDetailsByLibraryQuery>(request);

            var queryRequest = _mapper.Map<BookDetailsQueryRequest>(request);

            var queryResult = await  _unitOfWork.Books.GetBooksDetailsByLibraryAsync(queryRequest);

            var data = _mapper.Map<IEnumerable<BookDetailsViewModel>>(queryResult);

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<BookDetailsViewModel>>(paginatedData, "Livros recuperados com sucesso!", data.Count());

        }
    }
}
