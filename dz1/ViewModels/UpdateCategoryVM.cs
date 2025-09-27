using System.ComponentModel.DataAnnotations;

namespace dz1.ViewModels
{
    public class UpdateCategoryVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва категорії обов'язкова")]
        [StringLength(50, ErrorMessage = "Назва категорії не може перевищувати 50 символів")]
        public string? Name { get; set; }
    }
}
