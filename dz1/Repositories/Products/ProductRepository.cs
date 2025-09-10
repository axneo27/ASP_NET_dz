using Microsoft.EntityFrameworkCore;
using dz1.Models;
using Microsoft.AspNetCore.Http;

namespace dz1.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductRepository(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IEnumerable<Product> Products => _context.Products.Include(p => p.Category).ToList();

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public void Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    var imagePath = Path.Combine(_environment.WebRootPath, "images", product.Image);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Product> GetByCategory(int categoryId)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToList();
        }

        public Product? GetById(int id)
        {
            return _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetByPrice(decimal minPrice, decimal maxPrice)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToList();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public string SaveImage(IFormFile imageFile)
        {
            string uniqueFileName = string.Empty;
            if (imageFile != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
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