using spr421_spotify_clone.DAL.Entities;

namespace spr421_spotify_clone.DAL.Repositories.Artist
{
    public interface IArtistRepository : IGenericRepository<ArtistEntity>
    {
        IQueryable<ArtistEntity> Artists { get; }
        
        /// <summary>
        /// Отримати треки для конкретного виконавця
        /// </summary>
        /// <param name="artistId">ID виконавця</param>
        /// <returns>Список треків виконавця</returns>
        Task<List<TrackEntity>> GetTracksAsync(string artistId);
        
        /// <summary>
        /// Знайти виконавців за ім'ям
        /// </summary>
        /// <param name="name">Ім'я виконавця</param>
        /// <returns>Список виконавців з таким ім'ям</returns>
        Task<List<ArtistEntity>> GetByNameAsync(string name);
    }
}