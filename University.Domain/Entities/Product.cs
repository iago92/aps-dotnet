using System;
using System.ComponentModel.DataAnnotations;
using University.Domain.Validators;

namespace University.Domain.Entities
{
    /// <summary>
    /// Produto pertencente a uma categoria
    /// </summary>
    public class Product
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        [StringLength(200, MinimumLength = 3)]
        [ValidProductName]
        public required string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue)]
        [ValidPrice]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        // Chave estrangeira para Category
        [Required]
        public Guid CategoryId { get; set; }

        // Relacionamento com Category
        public virtual Category? Category { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // For EF Core
        protected Product() 
        {
            Name = string.Empty;
        }

        public Product(string name, string? description, decimal price, int stock, Guid categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
        }

        public void Update(string name, string? description, decimal price, int stock, Guid categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            CategoryId = categoryId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateStock(int quantity)
        {
            Stock = quantity;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
