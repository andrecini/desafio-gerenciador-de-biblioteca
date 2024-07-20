using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.AddLoan;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.RemoveLoan;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.UpdateLoan;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Commands.UpdateLoanStatus;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetAllLoans;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoanById;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansByFilter;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansByUser;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsByLibrary;
using Desafios.GerenciadorBiblioteca.Service.CQRS.Loans.Queries.GetLoansDetailsFiltered;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController(ILogger<LoansController> logger, IMediator mediator) : ControllerBase
    {
        private readonly ILogger<LoansController> _logger = logger;
        private readonly IMediator _mediator = mediator;


        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllLoansQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            GetLoanByIdQuery request = new(id);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId, int page, int size)
        {
            GetLoansByUserQuery request = new(page, size, userId);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetFiltered(GetLoansByFilterQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("library/{libraryId}/details/filter")]
        public async Task<IActionResult> GetFilteredDetails(int libraryId, GetLoansDetailsFilteredQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("library/{libraryId}/details")]
        public async Task<IActionResult> GetDetailsByLibrary(int libraryId, int page, int size)
        {
            GetLoansDetailsByLibraryQuery request = new(page, size, libraryId);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLoanCommand request)
        {
            var response = await _mediator.Send(request);

            var locationUri = Url.Link(nameof(GetById), new { id = response.Data.Id });

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateLoanCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateLoanStatusCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            RemoveLoanCommand request = new(id);

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
