using Microsoft.AspNetCore.Mvc;
using dz1.Models;
using dz1.Repositories.Categories;
using dz1.ViewModels;

namespace dz1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _categoryRepository.Categories;
            return View(categories);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken] // Захист від CSRF атак
        public IActionResult Create(CreateCategoryVM viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var res = _categoryRepository.IsExists(viewModel.Name!);
            if (res)
            {
                ModelState.AddModelError("Name", $"Категорія \"{viewModel.Name}\" вже існує");
                return View(viewModel);
            }

            var model = new Category
            {
                Name = viewModel.Name!
            };

            _categoryRepository.Create(model);
            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Update(int id)
        {
            var model = _categoryRepository.GetById(id);
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
        [ValidateAntiForgeryToken] // Захист від CSRF атак
        public IActionResult Update(UpdateCategoryVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var res = _categoryRepository.IsExists(viewModel.Name!);
            if (res)
            {
                ModelState.AddModelError("Name", $"Категорія \"{viewModel.Name}\" вже існує");
                return View(viewModel);
            }

            var model = new Category
            {
                Id = viewModel.Id,
                Name = viewModel.Name!
            };

            _categoryRepository.Update(model);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.GetById(id);

            if (category != null)
            {
                _categoryRepository.Delete(category);
            }
            
            return RedirectToAction("Index");
        }
    }
}