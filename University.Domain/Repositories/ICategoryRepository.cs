using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using University.Domain.Entities;

namespace University.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> SearchAsync(string searchTerm);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Guid id);
    }
}
