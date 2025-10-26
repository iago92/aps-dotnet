using System;
using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

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

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        // For EF Core
        protected Student() { }

        public Student(string firstName, string lastName, string email, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Age = age;
        }
    }
}
