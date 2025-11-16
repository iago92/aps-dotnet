using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using University.Application.Interfaces;
using University.Application.ViewModels;
using University.Domain.Entities;
using University.Domain.Repositories;

namespace University.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task CreateAsync(CategoryViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));

            var category = vm.Adapt<Category>();
            await _repository.AddAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID inválido.", nameof(id));
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Adapt<List<CategoryViewModel>>();
        }

        public async Task<CategoryViewModel?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID inválido.", nameof(id));

            var category = await _repository.GetByIdAsync(id);
            if (category == null) return null;

            return category.Adapt<CategoryViewModel>();
        }

        public async Task<IEnumerable<CategoryViewModel>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return await GetAllAsync();

            var categories = await _repository.SearchAsync(searchTerm);
            return categories.Adapt<List<CategoryViewModel>>();
        }

        public async Task UpdateAsync(CategoryViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));
            if (vm.Id == Guid.Empty) throw new ArgumentException("ID inválido.", nameof(vm));

            var existing = await _repository.GetByIdAsync(vm.Id);
            if (existing == null) throw new InvalidOperationException("Categoria não encontrada.");

            existing.Update(vm.Name, vm.Description);
            await _repository.UpdateAsync(existing);
        }
    }
}
