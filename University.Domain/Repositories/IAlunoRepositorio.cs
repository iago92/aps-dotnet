using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using University.Domain.Entities;

namespace University.Domain.Repositories
{
    public interface IAlunoRepositorio
    {
        Task<IEnumerable<Aluno>> GetAllAsync();
        Task<Aluno> GetByIdAsync(Guid id);
        Task AddAsync(Aluno aluno);
        Task UpdateAsync(Aluno aluno);
        Task DeleteAsync(Guid id);
    }
}
