using Desafios.GerenciadorBiblioteca.Domain.Enums;

namespace Desafios.GerenciadorBiblioteca.Website.Models.Responses
{
    public class TokenModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Token { get; set; }
        public Roles Role { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
