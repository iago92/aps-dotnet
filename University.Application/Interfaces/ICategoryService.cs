using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using University.Application.ViewModels;

namespace University.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
        Task<CategoryViewModel?> GetByIdAsync(Guid id);
        Task<IEnumerable<CategoryViewModel>> SearchAsync(string searchTerm);
        Task CreateAsync(CategoryViewModel vm);
        Task UpdateAsync(CategoryViewModel vm);
        Task DeleteAsync(Guid id);
    }
}
