namespace Desafios.GerenciadorBiblioteca.Website.Models.Responses
{
    public class CustomResponse<T>
    {
        public T Data { get; set; }
        public string Description { get; set; }
        public int Total { get; set; }
    }
}
