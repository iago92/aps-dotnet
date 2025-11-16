using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using University.Application.ViewModels;

namespace University.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();
        Task<IEnumerable<ProductViewModel>> GetAllByCategoryAsync(Guid categoryId);
        Task<ProductViewModel?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProductViewModel>> SearchAsync(string searchTerm);
        Task CreateAsync(ProductViewModel vm);
        Task UpdateAsync(ProductViewModel vm);
        Task DeleteAsync(Guid id);
    }
}
