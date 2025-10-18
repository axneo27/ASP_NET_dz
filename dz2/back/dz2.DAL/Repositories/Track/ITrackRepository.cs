using spr421_spotify_clone.DAL.Entities;

namespace spr421_spotify_clone.DAL.Repositories.Track
{
    public interface ITrackRepository
        : IGenericRepository<TrackEntity>
    {
        IQueryable<TrackEntity> Tracks { get; }
        
        /// <summary>
        /// Знайти треки за назвою
        /// </summary>
        /// <param name="title">Назва треку</param>
        /// <returns>Список треків з такою назвою</returns>
        Task<List<TrackEntity>> GetByTitleAsync(string title);
        
        /// <summary>
        /// Знайти треки за виконавцем
        /// </summary>
        /// <param name="artistId">ID виконавця</param>
        /// <returns>Список треків виконавця</returns>
        Task<List<TrackEntity>> GetByArtistAsync(string artistId);
        
        /// <summary>
        /// Знайти треки за жанром
        /// </summary>
        /// <param name="genreId">ID жанру</param>
        /// <returns>Список треків жанру</returns>
        Task<List<TrackEntity>> GetByGenreAsync(string genreId);
        
        /// <summary>
        /// Додати виконавця до треку
        /// </summary>
        /// <param name="trackId">ID треку</param>
        /// <param name="artistId">ID виконавця</param>
        Task AddArtistAsync(string trackId, string artistId);
    }
}
