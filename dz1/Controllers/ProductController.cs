using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using dz1.Models;
using dz1.Repositories.Products;
using dz1.ViewModels;

namespace dz1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var products = _repository.Products;
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_repository.GetCategories(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] CreateProductVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = new Product
                {
                    Name = viewModel.Name ?? string.Empty,
                    Description = viewModel.Description ?? string.Empty,
                    Price = (decimal)viewModel.Price,
                    CategoryId = viewModel.CategoryId,
                    Amount = viewModel.Amount
                };

                if (viewModel.Image != null)
                {
                    model.Image = _repository.SaveImage(viewModel.Image);
                }

                _repository.Create(model);
                TempData["Success"] = "Товар успішно створено";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_repository.GetCategories(), "Id", "Name");
            return View(viewModel);
        }

        public IActionResult Update(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = new SelectList(_repository.GetCategories(), "Id", "Name");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(product);
                TempData["Success"] = "Товар успішно оновлено";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_repository.GetCategories(), "Id", "Name");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            TempData["Success"] = "Товар успішно видалено";
            return RedirectToAction(nameof(Index));
        }
    }
}