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
    public class AlunoServico : IAlunoServico
    {
        private readonly IAlunoRepositorio _repositorio;

        public AlunoServico(IAlunoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task CreateAsync(AlunoViewModel vm)
        {
            var aluno = new Aluno(vm.PrimeiroNome, vm.Sobrenome, vm.Email, vm.Idade);
            await _repositorio.AddAsync(aluno);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repositorio.DeleteAsync(id);
        }

        public async Task<IEnumerable<AlunoViewModel>> GetAllAsync()
        {
            var all = await _repositorio.GetAllAsync();
            return all.Select(s => new AlunoViewModel
            {
                Id = s.Id,
                PrimeiroNome = s.PrimeiroNome,
                Sobrenome = s.Sobrenome,
                Email = s.Email,
                Idade = s.Idade,
                CriadoEm = s.CriadoEm
            });
        }

        public async Task<AlunoViewModel> GetByIdAsync(Guid id)
        {
            var s = await _repositorio.GetByIdAsync(id);
            if (s == null) return null;
            return new AlunoViewModel
            {
                Id = s.Id,
                PrimeiroNome = s.PrimeiroNome,
                Sobrenome = s.Sobrenome,
                Email = s.Email,
                Idade = s.Idade,
                CriadoEm = s.CriadoEm
            };
        }

        public async Task UpdateAsync(AlunoViewModel vm)
        {
            var existing = await _repositorio.GetByIdAsync(vm.Id);
            if (existing == null) throw new InvalidOperationException("Aluno não encontrado");

            existing.PrimeiroNome = vm.PrimeiroNome;
            existing.Sobrenome = vm.Sobrenome;
            existing.Email = vm.Email;
            existing.Idade = vm.Idade;

            await _repositorio.UpdateAsync(existing);
        }
    }
}
