using Microsoft.AspNetCore.Mvc;
using spr421_spotify_clone.BLL.Dtos.Genre;
using spr421_spotify_clone.BLL.Services.Genre;
using spr421_spotify_clone.Extensions;

namespace spr421_spotify_clone.Controllers
{
    [ApiController]
    [Route("api/genre")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateGenreDto dto)
        {
            var response = await _genreService.CreateAsync(dto);
            return this.ToActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateGenreDto dto)
        {
            var response = await _genreService.UpdateAsync(dto);
            return this.ToActionResult(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] string id)
        {
            var response = await _genreService.DeleteAsync(id);
            return this.ToActionResult(response);
        }

        [HttpGet("by-id")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] string id)
        {
            var response = await _genreService.GetByIdAsync(id);
            return this.ToActionResult(response);
        }

        [HttpGet("by-name")]
        public async Task<IActionResult> GetByNameAsync([FromQuery] string name)
        {
            var response = await _genreService.GetByNameAsync(name);
            return this.ToActionResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _genreService.GetAllAsync();
            return this.ToActionResult(response);
        }
    }
}
