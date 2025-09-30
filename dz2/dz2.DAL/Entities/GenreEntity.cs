﻿namespace spr421_spotify_clone.DAL.Entities
{
    public class GenreEntity : BaseEntity
    {
        public required string Name { get; set; }
        public required string NormalizedName { get; set; }

        public ICollection<TrackEntity> Tracks { get; set; } = [];
    }
}
