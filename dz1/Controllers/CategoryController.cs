using Microsoft.AspNetCore.Mvc;
using dz1.Models;
using dz1.Helpers;
using dz1.ViewModels;

namespace dz1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            var categories = _context.Categories.AsQueryable();
            var paginatedCategories = PaginatedList<Category>.Create(categories, pageNumber, pageSize);
            return View(paginatedCategories);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCategoryVM viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Check if category already exists
            var exists = _context.Categories.Any(c => c.Name == viewModel.Name);
            if (exists)
            {
                ModelState.AddModelError("Name", $"Категорія \"{viewModel.Name}\" вже існує");
                return View(viewModel);
            }

            var model = new Category
            {
                Name = viewModel.Name!
            };

            _context.Categories.Add(model);
            _context.SaveChanges();
            TempData["Success"] = $"Категорію '{model.Name}' успішно створено";
            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Update(int id)
        {
            var model = _context.Categories.Find(id);
            if(model == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = new UpdateCategoryVM
            {
                Id = model.Id,
                Name = model.Name
            };
            
            return View(viewModel);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdateCategoryVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Check if another category with same name exists
            var exists = _context.Categories.Any(c => c.Name == viewModel.Name && c.Id != viewModel.Id);
            if (exists)
            {
                ModelState.AddModelError("Name", $"Категорія \"{viewModel.Name}\" вже існує");
                return View(viewModel);
            }

            var model = _context.Categories.Find(viewModel.Id);
            if (model != null)
            {
                model.Name = viewModel.Name!;
                _context.SaveChanges();
                TempData["Success"] = $"Категорію '{model.Name}' успішно оновлено";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                var productCount = _context.Products.Count(p => p.CategoryId == id);
                _context.Categories.Remove(category);
                _context.SaveChanges();
                TempData["Success"] = $"Категорію '{category.Name}' та {productCount} пов'язаних товарів було видалено";
            }
            
            return RedirectToAction("Index");
        }
    }
}