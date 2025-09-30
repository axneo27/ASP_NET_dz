using Microsoft.AspNetCore.Http;

namespace spr421_spotify_clone.BLL.Services.Storage
{
    public class StorageService : IStorageService
    {
        public async Task<string?> SaveAudioFileAsync(IFormFile audioFile, string folderPath)
        {
            var types = audioFile.ContentType.Split("/");

            if (types.Length != 2 || types[0] != "audio")
            {
                return null;
            }

            string extenstion = Path.GetExtension(audioFile.FileName);
            string fileName = $"{Guid.NewGuid().ToString()}{extenstion}";
            string filePath = Path.Combine(folderPath, fileName);

            using (var fileStream = File.Create(filePath))
            {
                await audioFile.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}
