using Microsoft.EntityFrameworkCore;
using spr421_spotify_clone.DAL.Entities;

namespace spr421_spotify_clone.DAL.Repositories.Artist
{
    public class ArtistRepository : GenericRepository<ArtistEntity>, IArtistRepository
    {
        private readonly AppDbContext _context;

        public ArtistRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<ArtistEntity> Artists => GetAll();

        /// <summary>
        /// Отримати треки для конкретного виконавця
        /// </summary>
        /// <param name="artistId">ID виконавця</param>
        /// <returns>Список треків виконавця</returns>
        public async Task<List<TrackEntity>> GetTracksAsync(string artistId)
        {
            var artist = await _context.Artists
                .Include(a => a.Tracks)
                    .ThenInclude(t => t.Genre)
                .FirstOrDefaultAsync(a => a.Id == artistId);

            return artist?.Tracks.ToList() ?? new List<TrackEntity>();
        }

        /// <summary>
        /// Знайти виконавців за ім'ям
        /// </summary>
        /// <param name="name">Ім'я виконавця</param>
        /// <returns>Список виконавців з таким ім'ям</returns>
        public async Task<List<ArtistEntity>> GetByNameAsync(string name)
        {
            return await Artists
                .Where(a => a.Name.ToLower().Contains(name.ToLower()))
                .Include(a => a.Tracks)
                .ToListAsync();
        }
    }
}