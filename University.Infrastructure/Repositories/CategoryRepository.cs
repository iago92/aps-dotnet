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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly UniversityDbContext _context;

        public CategoryRepository(UniversityDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID inválido.", nameof(id));

            var category = await GetByIdAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(x => x.Products)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID inválido.", nameof(id));

            return await _context.Categories
                .Include(x => x.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Category>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            return await _context.Categories
                .Where(x => x.Name.Contains(searchTerm) || (x.Description != null && x.Description.Contains(searchTerm)))
                .Include(x => x.Products)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
