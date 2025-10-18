using AutoMapper;
using spr421_spotify_clone.BLL.Dtos.Track;
using spr421_spotify_clone.BLL.Services.Storage;
using spr421_spotify_clone.DAL.Entities;
using spr421_spotify_clone.DAL.Repositories.Genre;
using spr421_spotify_clone.DAL.Repositories.Track;
using System.Net;

namespace spr421_spotify_clone.BLL.Services.Track
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;

        public TrackService(ITrackRepository trackRepository, IMapper mapper, IGenreRepository genreRepository, IStorageService storageService)
        {
            _trackRepository = trackRepository;
            _mapper = mapper;
            _genreRepository = genreRepository;
            _storageService = storageService;
        }

        public async Task<ServiceResponse> CreateAsync(CreateTrackDto dto, string audioFilePath)
        {
            var entity = _mapper.Map<TrackEntity>(dto);

            var genre = await _genreRepository.GetByIdAsync(dto.GenreId);

            if(genre == null)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Жанр з id '{dto.GenreId}' не знайдено"
                };
            }

            entity.Genre = genre;

            var fileName = await _storageService.SaveAudioFileAsync(dto.AudioFile, audioFilePath);

            if(fileName == null)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Не вдалося зберегти файл"
                };
            }

            entity.AudioUrl = fileName;
            await _trackRepository.CreateAsync(entity);

            return new ServiceResponse
            {
                Message = $"Трек '{entity.Title}' додано"
            };
        }
        
            public async Task<ServiceResponse> UpdateAsync(UpdateTrackDto dto)
            {
                var track = await _trackRepository.GetByIdAsync(dto.Id);
                if (track == null)
                {
                    return new ServiceResponse
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Трек з id '{dto.Id}' не знайдено"
                    };
                }
                _mapper.Map(dto, track);
                await _trackRepository.UpdateAsync(track);
                return new ServiceResponse { Message = $"Трек '{track.Title}' оновлено" };
            }
        
            public async Task<ServiceResponse> DeleteAsync(DeleteTrackDto dto)
            {
                var track = await _trackRepository.GetByIdAsync(dto.Id);
                if (track == null)
                {
                    return new ServiceResponse
                    {
                        IsSuccess = false,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"Трек з id '{dto.Id}' не знайдено"
                    };
                }
                await _trackRepository.DeleteAsync(track);
                return new ServiceResponse { Message = $"Трек видалено" };
            }
        
            public async Task<ServiceResponse> GetByTitleAsync(string title)
            {
                var tracks = await _trackRepository.GetByTitleAsync(title);
                return new ServiceResponse { Data = tracks };
            }
        
            public async Task<ServiceResponse> GetByGenreAsync(string genreId)
            {
                var tracks = await _trackRepository.GetByGenreAsync(genreId);
                return new ServiceResponse { Data = tracks };
            }
    }
}
