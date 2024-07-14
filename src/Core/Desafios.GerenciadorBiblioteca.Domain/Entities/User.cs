using Desafios.GerenciadorBiblioteca.Domain.Entities.Base;
using Desafios.GerenciadorBiblioteca.Domain.Enums;
using System.Text.Json.Serialization;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public class User : IEntity<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password {  get; set; }
        public Roles Role { get; set; }

        [JsonIgnore]
        public ICollection<Loan>? Loans { get; set; }
    }
}
