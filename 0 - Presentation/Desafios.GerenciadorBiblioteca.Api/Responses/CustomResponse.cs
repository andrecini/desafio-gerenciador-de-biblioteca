namespace Desafios.GerenciadorBiblioteca.Api.Responses
{
    public class CustomResponse<T>
    {
        public CustomResponse(T data, string description)
        {
            Data = data;
            Description = description;
        }

        public T Data { get; set; }
        public string Description { get; set; }
    }
}
