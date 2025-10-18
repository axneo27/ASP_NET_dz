using spr421_spotify_clone.BLL.Dtos.Track;

namespace spr421_spotify_clone.BLL.Services.Track
{
    public interface ITrackService
    {
        Task<ServiceResponse> CreateAsync(CreateTrackDto dto, string audioFilePath);
        Task<ServiceResponse> UpdateAsync(UpdateTrackDto dto);
        Task<ServiceResponse> DeleteAsync(DeleteTrackDto dto);
        Task<ServiceResponse> GetByTitleAsync(string title);
        Task<ServiceResponse> GetByGenreAsync(string genreId);
    }
}
