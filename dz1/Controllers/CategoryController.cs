using Microsoft.AspNetCore.Mvc;
using dz1.Models;

namespace dz1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Categories;
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
        public IActionResult Create(Category model)
        {
            var res = _context.Categories
                .Any(c => c.Name.ToLower() == model.Name.ToLower());
            if(res)
            {
                return RedirectToAction("Index");
            }

            _context.Categories.Add(model);
            _context.SaveChanges();
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

            return View(model);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken] // Захист від CSRF атак
        public IActionResult Update(Category model)
        {
            var res = _context.Categories
                .Any(c => c.Name.ToLower() == model.Name.ToLower());
            if (res)
            {
                return RedirectToAction("Index");
            }

            _context.Categories.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST
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