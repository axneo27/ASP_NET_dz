using dz1.Models;

namespace dz1.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; } = [];
        public IEnumerable<Category> Categories { get; set; } = [];
    }
}
