using Microsoft.EntityFrameworkCore;
using spr421_spotify_clone.DAL.Entities;

namespace spr421_spotify_clone.DAL.Repositories.Genre
{
    public class GenreRepository
        : GenericRepository<GenreEntity>, IGenreRepository
    {
        public GenreRepository(AppDbContext context)
            : base(context) { }

        public IQueryable<GenreEntity> Genres => GetAll();

        public async Task<GenreEntity?> GetByNameAsync(string name)
        {
            return await Genres
                .FirstOrDefaultAsync(g => g.NormalizedName == name.ToUpper());
        }

        public Task<bool> IsExistsAsync(string name)
        {
            return Genres
                .AnyAsync(g => g.NormalizedName == name.ToUpper());
        }
    }
}
