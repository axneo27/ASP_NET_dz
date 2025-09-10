using dz1.Models;
using Microsoft.AspNetCore.Http;

namespace dz1.Repositories.Products
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        Product? GetById(int id);
        IEnumerable<Product> GetByCategory(int categoryId);
        IEnumerable<Product> GetByPrice(decimal minPrice, decimal maxPrice);
        void Create(Product product);
        void Update(Product product);
        void Delete(int id);
        string? SaveImage(IFormFile imageFile);
        IEnumerable<Category> GetCategories();
    }
}