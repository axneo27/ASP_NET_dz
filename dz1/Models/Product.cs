using System.ComponentModel.DataAnnotations;

namespace dz1.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва товару обов'язкова")]
        [StringLength(100, ErrorMessage = "Назва товару не може перевищувати 100 символів")]
        [Display(Name = "Назва")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Опис товару обов'язковий")]
        [Display(Name = "Опис")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ціна обов'язкова")]
        [Range(0.01, 1000000, ErrorMessage = "Ціна повинна бути більше 0 та менше 1000000")]
        [Display(Name = "Ціна")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Кількість обов'язкова")]
        [Range(0, int.MaxValue, ErrorMessage = "Кількість не може бути від'ємною")]
        [Display(Name = "Кількість")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Категорія обов'язкова")]
        [Display(Name = "Категорія")]
        public int CategoryId { get; set; }

        public string? Image { get; set; }
        
        public Category? Category { get; set; }
    }
}