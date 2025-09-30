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
    }
}
