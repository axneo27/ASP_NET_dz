using System.ComponentModel.DataAnnotations;

namespace spr421_spotify_clone.BLL.Dtos.Track
{
    public class DeleteTrackDto
    {
        [Required]
        public required string Id { get; set; }
    }
}
