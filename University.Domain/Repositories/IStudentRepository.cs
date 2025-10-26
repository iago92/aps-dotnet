using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using University.Domain.Entities;

namespace University.Domain.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(Guid id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Guid id);
    }
}
