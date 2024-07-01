using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Handlers
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not CustomException customException)
                return false;

            _logger.LogError("Aviso: {Message}", customException.ErrorDetails);

            var problemDetails = new ProblemDetails
            {
                Status = (int)customException.StatusCode,
                Title = "Falha ao Processar",
                Detail = customException.ErrorDetails
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

            return true;
        }
    }
}
