using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> CreateAsync(CreateGenreDto dto)
        {
            if(await _genreRepository.IsExistsAsync(dto.Name))
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = $"Жанр з назвою '{dto.Name}' вже існує",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var entity = _mapper.Map<GenreEntity>(dto);

            await _genreRepository.CreateAsync(entity);

            return new ServiceResponse
            {
                Message = $"Жанр '{dto.Name}' додано"
            };
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateGenreDto dto)
        {
            if (await _genreRepository.IsExistsAsync(dto.Name))
            {
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
                return new ServiceResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Жанр з id '{dto.Id}' не знайдено"
                };
            }

            entity = _mapper.Map(dto, entity);

            await _genreRepository.UpdateAsync(entity);

            return new ServiceResponse
            {
                Message = $"Жанр '{dto.Name}' оновлено"
            };
        }

        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);

            if(entity == null)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Жанр з id '{id}' не знайдено"
                };
            }

            await _genreRepository.DeleteAsync(entity);

            return new ServiceResponse
            {
                Message = $"Жанр '{entity.Name}' видалено"
            };
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var entities = await _genreRepository.Genres.ToListAsync();

            var dtos = _mapper.Map<List<GenreDto>>(entities);

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
            var entity = await _genreRepository.GetByIdAsync(id);

            if(entity == null)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Жанр з id '{id}' не знайдено"
                };
            }

            var dto = _mapper.Map<GenreDto>(entity);

            return new ServiceResponse
            {
                Message = "Жанр отримано",
                Payload = dto
            };
        }

        public async Task<ServiceResponse> GetByNameAsync(string name)
        {
            var entity = await _genreRepository.GetByNameAsync(name);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = $"Жанр '{name}' не знайдено"
                };
            }

            var dto = _mapper.Map<GenreDto>(entity);

            return new ServiceResponse
            {
                Message = "Жанр отримано",
                Payload = dto
            };
        }
    }
}
