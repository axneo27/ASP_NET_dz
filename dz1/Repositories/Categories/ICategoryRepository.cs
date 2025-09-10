using dz1.Models;

namespace dz1.Repositories.Categories
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        void Create(Category model);
        void Update(Category model);
        void Delete(Category model);
        Category? GetById(int id);
        Category? GetByName(string name);
        bool IsExists(string name);
    }
}
