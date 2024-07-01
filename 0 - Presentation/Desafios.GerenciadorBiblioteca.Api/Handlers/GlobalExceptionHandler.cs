using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Desafios.GerenciadorBiblioteca.Api.Handlers
{
    public class GlobalExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Ocorreu uma Exceção: {Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = httpContext.Response.StatusCode,
                Title = "Internal Server Error",
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

            return true;
        }
    }
}
