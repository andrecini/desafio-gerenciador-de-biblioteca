using Desafios.GerenciadorBiblioteca.Api.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace Desafios.GerenciadorBiblioteca.Api.Handlers
{
    public class GlobalExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Ocorreu uma Exceção: {Message}", exception.Message);

            var response = new CustomResponse<string[]>([exception.Message], "Exception");

            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken: cancellationToken);

            return true;
        }
    }
}
