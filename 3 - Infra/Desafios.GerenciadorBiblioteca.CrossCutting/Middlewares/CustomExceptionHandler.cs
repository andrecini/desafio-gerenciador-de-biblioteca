using Desafios.GerenciadorBiblioteca.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Web.Http.ExceptionHandling;


namespace Caramel.Pattern.Services.Api.Middlewares
{
    public class CustomExceptionHandler() : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not CustomException businessException)
                return false;

            //var response = new ExceptionResponse(
            //    businessException.Status,
            //    businessException.ErrorDetails
            //);

            //httpContext.Response.StatusCode = (int)businessException.StatusCode;
            //await httpContext.Response.WriteAsJsonAsync(response);

            return true;
        }
    }
}
