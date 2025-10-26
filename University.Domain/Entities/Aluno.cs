using System;
using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entities
{
    public class Aluno
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string PrimeiroNome { get; set; }

        [Required]
        [StringLength(100)]
        public string Sobrenome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; }

        [Range(16, 120)]
        public int Idade { get; set; }

        public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;

        // For EF Core
        protected Aluno() { }

        public Aluno(string primeiroNome, string sobrenome, string email, int idade)
        {
            PrimeiroNome = primeiroNome;
            Sobrenome = sobrenome;
            Email = email;
            Idade = idade;
        }
    }
}
