using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace spr421_spotify_clone.BLL.Dtos.Track
{
    public class CreateTrackDto
    {
        [Required(ErrorMessage = "Назва треку є обов'язковою")]
        public required string Title { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Аудіофайл є обов'язковим")]
        public required IFormFile AudioFile { get; set; }
        public string? PosterUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Жанр є обов'язковим")]
        public required string GenreId { get; set; }
    }
}
