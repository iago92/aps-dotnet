using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using University.Domain.Entities;

namespace University.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetAllByCategoryAsync(Guid categoryId);
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> SearchAsync(string searchTerm);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
