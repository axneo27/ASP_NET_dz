using Microsoft.AspNetCore.Http;

namespace spr421_spotify_clone.BLL.Services.Storage
{
    public interface IStorageService
    {
        Task<string?> SaveAudioFileAsync(IFormFile audioFile, string folderPath);
    }
}
