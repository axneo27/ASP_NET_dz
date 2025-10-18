using Microsoft.AspNetCore.Mvc;
using spr421_spotify_clone.BLL.Dtos.Track;
using spr421_spotify_clone.BLL.Services.Track;
using spr421_spotify_clone.Extensions;

namespace spr421_spotify_clone.Controllers
{
    [ApiController]
    [Route("api/track")]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;
        private readonly IWebHostEnvironment _environment;

        public TrackController(ITrackService trackService, IWebHostEnvironment environment)
        {
            _trackService = trackService;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateTrackDto dto)
        {
            var rootPath = _environment.ContentRootPath;
            var audioPath = Path.Combine(rootPath, "storage", "audio");

            var response = await _trackService.CreateAsync(dto, audioPath);
            return this.ToActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateTrackDto dto)
        {
            var response = await _trackService.UpdateAsync(dto);
            return this.ToActionResult(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteTrackDto dto)
        {
            var response = await _trackService.DeleteAsync(dto);
            return this.ToActionResult(response);
        }

        [HttpGet("by-title")]
        public async Task<IActionResult> GetByTitleAsync([FromQuery] string title)
        {
            var response = await _trackService.GetByTitleAsync(title);
            return this.ToActionResult(response);
        }

        [HttpGet("by-genre")]
        public async Task<IActionResult> GetByGenreAsync([FromQuery] string genreId)
        {
            var response = await _trackService.GetByGenreAsync(genreId);
            return this.ToActionResult(response);
        }
    }
}
