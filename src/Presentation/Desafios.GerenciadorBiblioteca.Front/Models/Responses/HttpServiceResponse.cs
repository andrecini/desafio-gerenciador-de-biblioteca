using System.Net;

namespace Desafios.GerenciadorBiblioteca.Website.Models.HttpService
{
    public class HttpServiceResponse<T>
    {
        public T Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string RawContent { get; set; }
        public string[] ErrorDetails { get; set; }
        public bool IsSuccess => ((int)StatusCode >= 200 && (int)StatusCode <= 299);
    }

    public class HttpServiceResponse
    {
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string RawContent { get; set; }
        public string[] ErrorDetails { get; set; }
        public bool IsSuccess => ((int)StatusCode >= 200 && (int)StatusCode <= 299);
    }
}
