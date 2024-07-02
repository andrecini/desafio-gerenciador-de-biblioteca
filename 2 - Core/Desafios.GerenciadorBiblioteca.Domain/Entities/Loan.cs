﻿using Desafios.GerenciadorBiblioteca.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities
{
    public class Loan : IEntity<int>
    {
        public int Id { get; set; }
        public int LibraryId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime LoanValidity { get; set; }
        public bool Returned { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public Book? Book { get; set; }
        [JsonIgnore]
        public Library? Library { get; set; }
    }
}
