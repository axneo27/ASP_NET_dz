using spr421_spotify_clone.BLL.Dtos.Auth;

namespace spr421_spotify_clone.BLL.Services.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse> LoginAsync(LoginDto dto); 
        Task<ServiceResponse> RegisterAsync(RegisterDto dto);
    }
}
