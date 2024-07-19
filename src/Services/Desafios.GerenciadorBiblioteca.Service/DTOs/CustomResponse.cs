namespace Desafios.GerenciadorBiblioteca.Service.DTOs
{
    public class CustomResponse<T>
    {
        public CustomResponse(T data, string description)
        {
            Data = data;
            Description = description;
        }

        public CustomResponse(T data, string description, int total)
        {
            Data = data;
            Description = description;
            Total = total;
        }

        public T Data { get; set; }
        public string Description { get; set; }
        public int Total { get; set; } = 1;
    }
}
