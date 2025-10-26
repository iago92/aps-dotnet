using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using University.Application.Interfaces;
using University.Application.ViewModels;

namespace University.WebUI.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IAlunoServico _servico;

        public AlunosController(IAlunoServico servico)
        {
            _servico = servico;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _servico.GetAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var vm = await _servico.GetByIdAsync(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _servico.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var vm = await _servico.GetByIdAsync(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AlunoViewModel vm)
        {
            if (id != vm.Id) return BadRequest();
            if (!ModelState.IsValid) return View(vm);
            await _servico.UpdateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var vm = await _servico.GetByIdAsync(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _servico.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
