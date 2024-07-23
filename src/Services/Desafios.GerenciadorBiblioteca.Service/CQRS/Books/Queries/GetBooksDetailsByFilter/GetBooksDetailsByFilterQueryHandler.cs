using AutoMapper;
using Desafios.GerenciadorBiblioteca.Domain.QueryModels.QueryRequests;
using Desafios.GerenciadorBiblioteca.Domain.UnitOfWork;
using Desafios.GerenciadorBiblioteca.Service.DTOs;
using Desafios.GerenciadorBiblioteca.Service.DTOs.ViewModels;
using Desafios.GerenciadorBiblioteca.Service.Helpers;
using MediatR;

namespace Desafios.GerenciadorBiblioteca.Service.CQRS.Books.Queries.GetBooksDetailsByFilter
{
    public class GetBooksDetailsByFilterQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetBooksDetailsByFilterQuery, CustomResponse<IEnumerable<BookDetailsViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomResponse<IEnumerable<BookDetailsViewModel>>> Handle(GetBooksDetailsByFilterQuery request, CancellationToken cancellationToken)
        {
            ValidatorHelper.ValidateEntity<GetBooksDetailsByFilterQueryValidator, GetBooksDetailsByFilterQuery>(request);

            var queryRequest = _mapper.Map<BookDetailsFilteredQueryRequest>(request);

            var queryResult = await _unitOfWork.Books.GetBooksDetailsByFilterAsync(queryRequest);

            var data = _mapper.Map<IEnumerable<BookDetailsViewModel>>(queryResult);

            var paginatedData = data.Paginate(request.Page, request.Size);

            return new CustomResponse<IEnumerable<BookDetailsViewModel>>(paginatedData, "Livros recuperados com sucesso!", data.Count());
        }
    }
}