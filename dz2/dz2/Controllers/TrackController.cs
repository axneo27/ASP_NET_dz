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
    }
}
