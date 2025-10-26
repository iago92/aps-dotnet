using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Application.Interfaces;
using University.Application.ViewModels;
using University.Domain.Entities;
using University.Domain.Repositories;

namespace University.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(StudentViewModel vm)
        {
            var student = new Student(vm.FirstName, vm.LastName, vm.Email, vm.Age);
            await _repository.AddAsync(student);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<StudentViewModel>> GetAllAsync()
        {
            var all = await _repository.GetAllAsync();
            return all.Select(s => new StudentViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Age = s.Age,
                CreatedAt = s.CreatedAt
            });
        }

        public async Task<StudentViewModel> GetByIdAsync(Guid id)
        {
            var s = await _repository.GetByIdAsync(id);
            if (s == null) return null;
            return new StudentViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Age = s.Age,
                CreatedAt = s.CreatedAt
            };
        }

        public async Task UpdateAsync(StudentViewModel vm)
        {
            var existing = await _repository.GetByIdAsync(vm.Id);
            if (existing == null) throw new InvalidOperationException("Student not found");

            // Update mutable fields
            existing.FirstName = vm.FirstName;
            existing.LastName = vm.LastName;
            existing.Email = vm.Email;
            existing.Age = vm.Age;

            await _repository.UpdateAsync(existing);
        }
    }
}
