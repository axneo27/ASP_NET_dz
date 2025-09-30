using spr421_spotify_clone.DAL.Entities;

namespace spr421_spotify_clone.DAL.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        Task CreateAsync(TEntity entity);
        Task CreateRangeAsync(params TEntity[] entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(string id);
        IQueryable<TEntity> GetAll();
    }
}
