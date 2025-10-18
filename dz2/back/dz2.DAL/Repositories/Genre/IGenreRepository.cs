using spr421_spotify_clone.DAL.Entities;

namespace spr421_spotify_clone.DAL.Repositories.Genre
{
    public interface IGenreRepository
        : IGenericRepository<GenreEntity>
    {
        IQueryable<GenreEntity> Genres { get; }
        Task<GenreEntity?> GetByNameAsync(string name);
        Task<bool> IsExistsAsync(string name);
    }
}
