using Desafios.GerenciadorBiblioteca.Api.Responses;
using Desafios.GerenciadorBiblioteca.Domain.Entities;
using Desafios.GerenciadorBiblioteca.Domain.Entities.Filters;
using Desafios.GerenciadorBiblioteca.Service.DTOs.Requests;
using Desafios.GerenciadorBiblioteca.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILogger<LoanController> _logger;
        private readonly ILoanService _service;

        public LoanController(ILogger<LoanController> logger, ILoanService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            var response = new CustomResponse<IEnumerable<Loan>>(data, "Empréstimos Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            var response = new CustomResponse<Loan>(data, "Empréstimo Recupedado com Sucesso!");

            return data != null ? Ok(response) : NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var data = await _service.GetByUserAsync(userId);
            var response = new CustomResponse<IEnumerable<Loan>>(data, "Empréstimos Recupedado com Sucesso!");

            return data != null ? Ok(response) : NoContent();
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(LoanFilter filter)
        {
            var data = await _service.GetByFilterAsync(filter);
            var response = new CustomResponse<IEnumerable<Loan>>(data, "Empréstimos Recupedados com Sucesso!");

            return data.Any() ? Ok(response) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Add(LoanInputModel request)
        {
            var data = await _service.AddAsync(request);
            var response = new CustomResponse<Loan>(data, "Empréstimo Adicionado com Sucesso!");

            var locationUri = Url.Link(nameof(GetById), new { id = data.Id });

            return Created(locationUri, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LoanInputModel request)
        {
            var data = await _service.UpdateAsync(id, request);
            var response = new CustomResponse<Loan>(data, "Empréstimo Atualizado com Sucesso!");

            return Ok(response);
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, bool returned)
        {
            var data = await _service.UpdateStatusAsync(id, returned);
            var response = new CustomResponse<Loan>(data, "Empréstimo Atualizado com Sucesso!");

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var data = await _service.RemoveAsync(id);
            var response = new CustomResponse<bool>(data, "Empréstimo Removido com Sucesso!");

            return Ok(response);
        }
    }
}
