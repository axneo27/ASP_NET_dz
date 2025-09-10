using Microsoft.EntityFrameworkCore;
using dz1.Models;

namespace dz1.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories => _context.Categories;

        public void Create(Category model)
        {
            _context.Categories.Add(model);
            _context.SaveChanges();
        }

        public void Delete(Category model)
        {
            _context.Categories.Remove(model);
            _context.SaveChanges();
        }

        public Category? GetById(int id)
        {
            return _context.Categories
                .FirstOrDefault(c => c.Id == id);
        }

        public Category? GetByName(string name)
        {
            return _context.Categories
                .FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
        }

        public bool IsExists(string name)
        {
            return _context.Categories
                .Any(c => c.Name.ToLower() == name.ToLower());
        }

        public void Update(Category model)
        {
            _context.Categories.Update(model);
            _context.SaveChanges();
        }
    }
}
