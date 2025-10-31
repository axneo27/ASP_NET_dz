using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using spr421_spotify_clone.BLL.Dtos.Genre;
using spr421_spotify_clone.DAL.Entities;
using spr421_spotify_clone.DAL.Repositories.Genre;
using System.Net;

namespace spr421_spotify_clone.BLL.Services.Genre
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GenreService> _logger;

        public GenreService(IGenreRepository genreRepository, IMapper mapper, ILogger<GenreService> logger)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse> CreateAsync(CreateGenreDto dto)
        {
            _logger.LogInformation("Створення жанру розпочато: {GenreName}", dto.Name);
            
            if(await _genreRepository.IsExistsAsync(dto.Name))
            {
                _logger.LogWarning("Спроба створити жанр з назвою що вже існує: {GenreName}", dto.Name);
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = $"Жанр з назвою '{dto.Name}' вже існує",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var entity = _mapper.Map<GenreEntity>(dto);

            await _genreRepository.CreateAsync(entity);

            _logger.LogInformation("Жанр успішно створено: {GenreName} з ID: {GenreId}", dto.Name, entity.Id);
            
            return new ServiceResponse
            {
                Message = $"Жанр '{dto.Name}' додано"
            };
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateGenreDto dto)
        {
            _logger.LogInformation("Оновлення жанру розпочато: ID {GenreId}, нова назва: {GenreName}", dto.Id, dto.Name);
            
            if (await _genreRepository.IsExistsAsync(dto.Name))
            {
                _logger.LogWarning("Спроба оновити жанр на назву що вже існує: {GenreName}", dto.Name);
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = $"Жанр з назвою '{dto.Name}' вже існує",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var entity = await _genreRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                _logger.LogWarning("Спроба оновити неіснуючий жанр з ID: {GenreId}", dto.Id);
                return new ServiceResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Жанр з id '{dto.Id}' не знайдено"
                };
            }

            var oldName = entity.Name;
            entity = _mapper.Map(dto, entity);

            await _genreRepository.UpdateAsync(entity);

            _logger.LogInformation("Жанр успішно оновлено: '{OldName}' -> '{NewName}' (ID: {GenreId})", 
                oldName, dto.Name, dto.Id);

            return new ServiceResponse
            {
                Message = $"Жанр '{dto.Name}' оновлено"
            };
        }

        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            _logger.LogInformation("Видалення жанру розпочато: ID {GenreId}", id);
            
            var entity = await _genreRepository.GetByIdAsync(id);

            if(entity == null)
            {
                _logger.LogWarning("Спроба видалити неіснуючий жанр з ID: {GenreId}", id);
                return new ServiceResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Жанр з id '{id}' не знайдено"
                };
            }

            var genreName = entity.Name;
            await _genreRepository.DeleteAsync(entity);

            _logger.LogInformation("Жанр успішно видалено: '{GenreName}' (ID: {GenreId})", genreName, id);

            return new ServiceResponse
            {
                Message = $"Жанр '{entity.Name}' видалено"
            };
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            _logger.LogInformation("Отримання всіх жанрів розпочато");
            
            var entities = await _genreRepository.Genres.ToListAsync();

            var dtos = _mapper.Map<List<GenreDto>>(entities);

            _logger.LogInformation("Успішно отримано {GenreCount} жанрів", entities.Count);

            return new ServiceResponse
            {
                Message = "Жанри отримано",
                Payload = dtos
            };

            //List<GenreDto> dtos = [];

            //foreach (var entity in entities)
            //{
            //    var dto = new GenreDto
            //    {
            //        Id = entity.Id,
            //        Name = entity.Name
            //    };
            //    dtos.Add(dto);
            //}
        }

        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            _logger.LogInformation("Отримання жанру за ID розпочато: {GenreId}", id);
            
            var entity = await _genreRepository.GetByIdAsync(id);

            if(entity == null)
            {
                _logger.LogWarning("Жанр з ID не знайдено: {GenreId}", id);
                return new ServiceResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Жанр з id '{id}' не знайдено"
                };
            }

            var dto = _mapper.Map<GenreDto>(entity);

            _logger.LogInformation("Жанр успішно знайдено: '{GenreName}' (ID: {GenreId})", entity.Name, id);

            return new ServiceResponse
            {
                Message = "Жанр отримано",
                Payload = dto
            };
        }

        public async Task<ServiceResponse> GetByNameAsync(string name)
        {
            _logger.LogInformation("Отримання жанру за назвою розпочато: {GenreName}", name);
            
            var entity = await _genreRepository.GetByNameAsync(name);

            if (entity == null)
            {
                _logger.LogWarning("Жанр з назвою не знайдено: {GenreName}", name);
                return new ServiceResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Жанр '{name}' не знайдено"
                };
            }

            var dto = _mapper.Map<GenreDto>(entity);

            _logger.LogInformation("Жанр успішно знайдено за назвою: '{GenreName}' (ID: {GenreId})", name, entity.Id);

            return new ServiceResponse
            {
                Message = "Жанр отримано",
                Payload = dto
            };
        }
    }
}
