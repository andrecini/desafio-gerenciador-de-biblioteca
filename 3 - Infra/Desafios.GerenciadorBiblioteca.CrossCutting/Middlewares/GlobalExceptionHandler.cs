using Caramel.Pattern.Services.Domain.Enums;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.ExceptionHandling;

namespace Caramel.Pattern.Services.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler

        public GlobalExceptionHandler() { 
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Ocorreu uma Exceção: {Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = (int)StatusProcess.Failure,
                Title = "Internal Server Error",
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);

            return true;
        }
    }
}
