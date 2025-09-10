using System.ComponentModel.DataAnnotations;

namespace dz1.ViewModels
{
    public class UpdateCategoryVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Вкажіть назву категорії")]
        [MaxLength(50, ErrorMessage = "Максимальна довжина 50 символів")]
        public string? Name { get; set; }
    }
}
