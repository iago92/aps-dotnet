using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using University.Application.Interfaces;
using University.Application.ViewModels;

namespace University.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(vm);
            }

            try
            {
                await _productService.CreateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("CategoryId", ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao criar produto: {ex.Message}");
            }

            var categoriesList = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesList, "Id", "Name");
            return View(vm);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel vm)
        {
            if (id != vm.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name", vm.CategoryId);
                return View(vm);
            }

            try
            {
                await _productService.UpdateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao atualizar produto: {ex.Message}");
            }

            var categoriesList = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoriesList, "Id", "Name", vm.CategoryId);
            return View(vm);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao deletar produto: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Products/Search
        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var products = await _productService.SearchAsync(searchTerm);
            return PartialView("_ProductList", products);
        }

        // GET: Products/ByCategory/5
        public async Task<IActionResult> ByCategory(Guid categoryId)
        {
            var products = await _productService.GetAllByCategoryAsync(categoryId);
            return PartialView("_ProductList", products);
        }
    }
}
