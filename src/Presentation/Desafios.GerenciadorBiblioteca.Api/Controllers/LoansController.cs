using Desafios.GerenciadorBiblioteca.Api.Responses;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Responses;
using Desafios.GerenciadorBiblioteca.Service.Libraries.Queries.GetLibraryById;
using Desafios.GerenciadorBiblioteca.Service.Loans.Commands.AddLoan;
using Desafios.GerenciadorBiblioteca.Service.Loans.Commands.RemoveLoan;
using Desafios.GerenciadorBiblioteca.Service.Loans.Commands.UpdateLoan;
using Desafios.GerenciadorBiblioteca.Service.Loans.Commands.UpdateLoanStatus;
using Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetAllLoans;
using Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetFilteredLoanDetails;
using Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetLoanById;
using Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetLoansByFilter;
using Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetLoansByUser;
using Desafios.GerenciadorBiblioteca.Service.Loans.Queries.GetLoansDetailsByLibrary;
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
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<Loan>>(data, "Empréstimos Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, GetLoanByIdQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Loan>(data, "Empréstimo Recupedado com Sucesso!");

            return data != null ? Ok(response) : NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId, GetLoansByUserQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<Loan>>(data, "Empréstimos Recupedado com Sucesso!");

            return data != null ? Ok(response) : NoContent();
        }

        [HttpPost("filter")]
        public async Task<IActionResult> GetFiltered(GetLoansByFilterQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<Loan>>(data, "Empréstimos Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpPost("filter/details")]
        public async Task<IActionResult> GEtFilteredDetails(GetFilteredLoanDetailsQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<LoanDetailsViewModel>>(data, "Empréstimos Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpPost("details")]
        public async Task<IActionResult> GetDetailsByLibrary(GetLoansDetailsByLibraryQuery request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<IEnumerable<LoanDetailsViewModel>>(data, "Empréstimos Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLoanCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Loan>(data, "Empréstimo Adicionado com Sucesso!");

            var locationUri = Url.Link(nameof(GetById), new { id = data.Id });

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateLoanCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Loan>(data, "Empréstimo Atualizado com Sucesso!");

            return Ok(response);
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateLoanStatusCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<Loan>(data, "Empréstimo Atualizado com Sucesso!");

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id, RemoveLoanCommand request)
        {
            var data = await _mediator.Send(request);

            var response = new CustomResponse<bool>(data, "Empréstimo Removido com Sucesso!");

            return Ok(response);
        }
    }
}
