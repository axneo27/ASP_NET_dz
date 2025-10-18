using System.ComponentModel.DataAnnotations;

namespace spr421_spotify_clone.BLL.Dtos.Genre
{
    public class UpdateGenreDto
    {
        [Required(ErrorMessage = "Поле 'Id' є обов'зяковим")]
        public required string Id { get; set; }
        [Required(ErrorMessage = "Ім'я жанру є обов'язковим")]
        [MinLength(3, ErrorMessage = "Повинно бути мінімум 3 символи")]
        public required string Name { get; set; }
    }
}
