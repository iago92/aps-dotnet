using System;
using System.ComponentModel.DataAnnotations;

namespace University.Application.ViewModels
{
    public class AlunoViewModel
    {
        public Guid Id { get; set; }

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

        public DateTime CriadoEm { get; set; }
    }
}
