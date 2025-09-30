using spr421_spotify_clone.BLL.Dtos.Genre;

namespace spr421_spotify_clone.BLL.Services.Genre
{
    public interface IGenreService
    {
        Task<ServiceResponse> CreateAsync(CreateGenreDto dto);
        Task<ServiceResponse> UpdateAsync(UpdateGenreDto dto);
        Task<ServiceResponse> DeleteAsync(string id);
        Task<ServiceResponse> GetByIdAsync(string id);
        Task<ServiceResponse> GetByNameAsync(string name);
        Task<ServiceResponse> GetAllAsync();
    }
}
