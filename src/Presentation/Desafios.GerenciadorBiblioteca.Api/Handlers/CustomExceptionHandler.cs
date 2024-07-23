using Desafios.GerenciadorBiblioteca.Api.Responses;
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

            var response = new CustomResponse<string[]>(customException.ErrorDetails, "CustomException");

            httpContext.Response.StatusCode = (int)customException.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken: cancellationToken);

            return true;
        }
    }
}
