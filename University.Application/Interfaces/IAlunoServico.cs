using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using University.Application.ViewModels;

namespace University.Application.Interfaces
{
    public interface IAlunoServico
    {
        Task<IEnumerable<AlunoViewModel>> GetAllAsync();
        Task<AlunoViewModel> GetByIdAsync(Guid id);
        Task CreateAsync(AlunoViewModel vm);
        Task UpdateAsync(AlunoViewModel vm);
        Task DeleteAsync(Guid id);
    }
}
