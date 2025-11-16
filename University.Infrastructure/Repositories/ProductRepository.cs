using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using University.Domain.Entities;
using University.Domain.Repositories;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly UniversityDbContext _context;

        public ProductRepository(UniversityDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID inválido.", nameof(id));

            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByCategoryAsync(Guid categoryId)
        {
            if (categoryId == Guid.Empty) throw new ArgumentException("ID inválido.", nameof(categoryId));

            return await _context.Products
                .Where(x => x.CategoryId == categoryId)
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID inválido.", nameof(id));

            return await _context.Products
                .Include(x => x.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            return await _context.Products
                .Where(x => x.Name.Contains(searchTerm) || (x.Description != null && x.Description.Contains(searchTerm)))
                .Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
