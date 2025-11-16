using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace University.Domain.Entities
{
    /// <summary>
    /// Categoria de produtos
    /// </summary>
    public class Category
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Relacionamento 1:N com Product
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        // For EF Core
        protected Category() { }

        public Category(string name, string description = null)
        {
            Name = name;
            Description = description;
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
