using System;
using System.ComponentModel.DataAnnotations;

namespace University.Application.ViewModels
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; }

        [Range(16, 120)]
        public int Age { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
