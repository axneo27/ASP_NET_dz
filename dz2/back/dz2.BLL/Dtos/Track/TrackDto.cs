namespace spr421_spotify_clone.BLL.Dtos.Track
{
    public class TrackDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string AudioUrl { get; set; } = string.Empty;
        public string? PosterUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string GenreId { get; set; } = string.Empty;
        public string GenreName { get; set; } = string.Empty;
    }
}
