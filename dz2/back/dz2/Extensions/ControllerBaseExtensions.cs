using Microsoft.AspNetCore.Mvc;
using spr421_spotify_clone.BLL.Services;

namespace spr421_spotify_clone.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult ToActionResult(this ControllerBase controller, ServiceResponse response)
        {
            return controller.StatusCode((int)response.StatusCode, response);
        }
    }
}
