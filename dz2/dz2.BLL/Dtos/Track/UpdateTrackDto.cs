using System.ComponentModel.DataAnnotations;

namespace spr421_spotify_clone.BLL.Dtos.Track
{
    public class UpdateTrackDto
    {
        [Required]
        public required string Id { get; set; }
        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        public string? PosterUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        [Required]
        public required string GenreId { get; set; }
    }
}
