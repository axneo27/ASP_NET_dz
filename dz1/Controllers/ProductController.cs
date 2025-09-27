using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dz1.Models;
using dz1.Helpers;
using dz1.ViewModels;

namespace dz1.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
        {
            var products = _context.Products.Include(p => p.Category);
            var paginatedProducts = PaginatedList<Product>.Create(products, pageNumber, pageSize);
            return View(paginatedProducts);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateProductVM
            {
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
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
                    model.Image = SaveImage(viewModel.Image);
                }

                _context.Products.Add(model);
                _context.SaveChanges();
                TempData["Success"] = "Товар успішно створено";
                return RedirectToAction("Index");
            }

            viewModel.Categories = _context.Categories.ToList();
            return View(viewModel);
        }

        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(product);
                _context.SaveChanges();
                TempData["Success"] = "Товар успішно оновлено";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                // Delete image file if exists
                if (!string.IsNullOrEmpty(product.Image))
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", product.Image);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.Products.Remove(product);
                _context.SaveChanges();
                TempData["Success"] = $"Товар '{product.Name}' було видалено";
            }
            return RedirectToAction("Index");
        }

        private string SaveImage(IFormFile imageFile)
        {
            string uniqueFileName = string.Empty;
            if (imageFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                Directory.CreateDirectory(uploadsFolder); // Ensure directory exists
                uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}