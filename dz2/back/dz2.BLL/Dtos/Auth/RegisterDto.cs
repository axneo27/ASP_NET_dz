using System.ComponentModel.DataAnnotations;

namespace spr421_spotify_clone.BLL.Dtos.Auth
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
