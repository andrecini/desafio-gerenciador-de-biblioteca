using System.Net;

namespace Desafios.GerenciadorBiblioteca.Domain.Exceptions
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public object ErrorDetails { get; set; }

        public CustomException(object details, HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            ErrorDetails = details;
        }

        public static void ThrowIfNull(object argument, string argumentName)
        {
            if (argument == null)
                throw new CustomException(
                    $"O parâmetro {argumentName} não pode ser nulo.",
                    HttpStatusCode.UnprocessableEntity
                );
        }
    }
}
