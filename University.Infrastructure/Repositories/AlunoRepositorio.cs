using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using University.Domain.Entities;
using University.Domain.Repositories;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories
{
    public class AlunoRepositorio : IAlunoRepositorio
    {
        private readonly UniversityDbContext _context;

        public AlunoRepositorio(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var s = await _context.Alunos.FindAsync(id);
            if (s == null) return;
            _context.Alunos.Remove(s);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Aluno>> GetAllAsync()
        {
            return await _context.Alunos.AsNoTracking().ToListAsync();
        }

        public async Task<Aluno> GetByIdAsync(Guid id)
        {
            return await _context.Alunos.FindAsync(id);
        }

        public async Task UpdateAsync(Aluno aluno)
        {
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
        }
    }
}
