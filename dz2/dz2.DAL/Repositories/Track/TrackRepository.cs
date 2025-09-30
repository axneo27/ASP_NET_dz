using Microsoft.EntityFrameworkCore;
using spr421_spotify_clone.DAL.Entities;

namespace spr421_spotify_clone.DAL.Repositories.Track
{
    public class TrackRepository 
        : GenericRepository<TrackEntity>, ITrackRepository
    {
        private readonly AppDbContext _context;

        public TrackRepository(AppDbContext context)
            : base(context) 
        {
            _context = context;
        }

        public IQueryable<TrackEntity> Tracks => GetAll();

        /// <summary>
        /// Знайти треки за назвою
        /// </summary>
        /// <param name="title">Назва треку</param>
        /// <returns>Список треків з такою назвою</returns>
        public async Task<List<TrackEntity>> GetByTitleAsync(string title)
        {
            return await Tracks
                .Where(t => t.Title.ToLower().Contains(title.ToLower()))
                .Include(t => t.Genre)
                .Include(t => t.Artists)
                .ToListAsync();
        }

        /// <summary>
        /// Знайти треки за виконавцем
        /// </summary>
        /// <param name="artistId">ID виконавця</param>
        /// <returns>Список треків виконавця</returns>
        public async Task<List<TrackEntity>> GetByArtistAsync(string artistId)
        {
            return await Tracks
                .Where(t => t.Artists.Any(a => a.Id == artistId))
                .Include(t => t.Genre)
                .Include(t => t.Artists)
                .ToListAsync();
        }

        /// <summary>
        /// Знайти треки за жанром
        /// </summary>
        /// <param name="genreId">ID жанру</param>
        /// <returns>Список треків жанру</returns>
        public async Task<List<TrackEntity>> GetByGenreAsync(string genreId)
        {
            return await Tracks
                .Where(t => t.GenreId == genreId)
                .Include(t => t.Genre)
                .Include(t => t.Artists)
                .ToListAsync();
        }

        /// <summary>
        /// Додати виконавця до треку
        /// </summary>
        /// <param name="trackId">ID треку</param>
        /// <param name="artistId">ID виконавця</param>
        public async Task AddArtistAsync(string trackId, string artistId)
        {
            var track = await _context.Tracks
                .Include(t => t.Artists)
                .FirstOrDefaultAsync(t => t.Id == trackId);

            var artist = await _context.Artists
                .FirstOrDefaultAsync(a => a.Id == artistId);

            if (track != null && artist != null)
            {
                // Перевіряємо, чи виконавець вже додний до треку
                if (!track.Artists.Any(a => a.Id == artistId))
                {
                    track.Artists.Add(artist);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
