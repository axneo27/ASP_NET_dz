using dz1.Models;
using System.ComponentModel.DataAnnotations;

namespace dz1.ViewModels
{
    public class CreateProductVM
    {
        [Required(ErrorMessage = "Назва товару обов'язкова")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "Опис товару обов'язковий")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Ціна обов'язкова")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Ціна повинна бути більше 0")]
        public double Price { get; set; }
        
        [Required(ErrorMessage = "Кількість обов'язкова")]
        [Range(0, int.MaxValue, ErrorMessage = "Кількість не може бути від'ємною")]
        public int Amount { get; set; }
        
        [Required(ErrorMessage = "Категорія обов'язкова")]
        public int CategoryId { get; set; }
        
        public IFormFile? Image { get; set; }
        public IEnumerable<Category> Categories { get; set; } = [];
    }
}
