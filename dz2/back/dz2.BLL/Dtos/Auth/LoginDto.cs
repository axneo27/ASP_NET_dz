using System.ComponentModel.DataAnnotations;

namespace spr421_spotify_clone.BLL.Dtos.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Поле 'Login' є обов'язковим")]
        public required string Login { get; set; }
        [Required(ErrorMessage = "Поле 'Password' є обов'язковим")]
        public required string Password { get; set; }
    }
}
