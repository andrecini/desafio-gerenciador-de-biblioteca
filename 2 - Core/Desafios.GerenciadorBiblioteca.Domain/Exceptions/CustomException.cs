using System.Net;

namespace Desafios.GerenciadorBiblioteca.Domain.Exceptions
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorDetails { get; set; }

        public CustomException(string details, HttpStatusCode statusCode)
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

        public static void ThrowIfLessThan(int argument, string argumentName)
        {
            if (argument < 0)
                throw new CustomException(
                    $"O parâmetro {argumentName} deve ser maior ou igual a 0.",
                    HttpStatusCode.UnprocessableEntity
                );
        }
    }
}
