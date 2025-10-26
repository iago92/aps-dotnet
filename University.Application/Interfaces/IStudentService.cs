using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using University.Application.ViewModels;

namespace University.Application.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentViewModel>> GetAllAsync();
        Task<StudentViewModel> GetByIdAsync(Guid id);
        Task CreateAsync(StudentViewModel vm);
        Task UpdateAsync(StudentViewModel vm);
        Task DeleteAsync(Guid id);
    }
}
