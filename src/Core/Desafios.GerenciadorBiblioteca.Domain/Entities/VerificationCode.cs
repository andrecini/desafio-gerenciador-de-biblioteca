using Desafios.GerenciadorBiblioteca.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public class VerificationCode : IEntity<int>
    {
        public int Id { get ; set; }
        public int UserId { get; set; }
        public string? Code { get; set; }
        public DateTime ValidTo { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
