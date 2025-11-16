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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task CreateAsync(ProductViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));

            // Validar se categoria existe
            var category = await _categoryRepository.GetByIdAsync(vm.CategoryId);
            if (category == null) throw new InvalidOperationException("Categoria não encontrada.");

            var product = vm.Adapt<Product>();
            await _repository.AddAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID inválido.", nameof(id));
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Adapt<List<ProductViewModel>>();
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllByCategoryAsync(Guid categoryId)
        {
            if (categoryId == Guid.Empty) throw new ArgumentException("ID inválido.", nameof(categoryId));

            var products = await _repository.GetAllByCategoryAsync(categoryId);
            return products.Adapt<List<ProductViewModel>>();
        }

        public async Task<ProductViewModel?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID inválido.", nameof(id));

            var product = await _repository.GetByIdAsync(id);
            if (product == null) return null;

            return product.Adapt<ProductViewModel>();
        }

        public async Task<IEnumerable<ProductViewModel>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return await GetAllAsync();

            var products = await _repository.SearchAsync(searchTerm);
            return products.Adapt<List<ProductViewModel>>();
        }

        public async Task UpdateAsync(ProductViewModel vm)
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));
            if (vm.Id == Guid.Empty) throw new ArgumentException("ID inválido.", nameof(vm));

            var existing = await _repository.GetByIdAsync(vm.Id);
            if (existing == null) throw new InvalidOperationException("Produto não encontrado.");

            // Validar se categoria existe
            var category = await _categoryRepository.GetByIdAsync(vm.CategoryId);
            if (category == null) throw new InvalidOperationException("Categoria não encontrada.");

            existing.Update(vm.Name, vm.Description, vm.Price, vm.Stock, vm.CategoryId);
            await _repository.UpdateAsync(existing);
        }
    }
}
