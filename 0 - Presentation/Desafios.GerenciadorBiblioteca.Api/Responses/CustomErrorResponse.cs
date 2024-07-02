namespace Desafios.GerenciadorBiblioteca.Api.Responses
{
    public class CustomErrorResponse
    {
        public CustomErrorResponse(object details, string type)
        {
            ErrorDatails = details;
            ExceptionType = type;
        }

        public object ErrorDatails { get; set; }
        public string ExceptionType { get; set; }
    }
}
