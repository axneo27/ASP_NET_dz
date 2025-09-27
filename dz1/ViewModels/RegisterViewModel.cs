using System.ComponentModel.DataAnnotations;

namespace dz1.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ім'я обов'язкове")]
        [StringLength(50, ErrorMessage = "Ім'я не може бути довшим за 50 символів")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Прізвище обов'язкове")]
        [StringLength(50, ErrorMessage = "Прізвище не може бути довшим за 50 символів")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ім'я користувача обов'язкове")]
        [StringLength(50, ErrorMessage = "Ім'я користувача не може бути довшим за 50 символів")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email обов'язковий")]
        [EmailAddress(ErrorMessage = "Некоректний формат email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обов'язковий")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль повинен бути від 6 до 100 символів")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Підтвердження паролю обов'язкове")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
