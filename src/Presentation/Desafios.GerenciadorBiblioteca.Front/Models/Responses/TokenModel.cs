using Desafios.GerenciadorBiblioteca.Domain.Enums;

namespace Desafios.GerenciadorBiblioteca.Website.Models.Responses
{
    public class TokenModel
    {
        public TokenModel() { }

        public TokenModel(int id, string? name, string? token, Roles role, DateTime validTo)
        {
            Id = id;
            Name = name;
            Token = token;
            Role = role;
            ValidTo = validTo;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Token { get; set; }
        public Roles Role { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
