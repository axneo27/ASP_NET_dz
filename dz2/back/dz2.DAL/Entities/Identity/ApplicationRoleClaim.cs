using Microsoft.AspNetCore.Identity;

namespace spr421_spotify_clone.DAL.Entities.Identity
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public virtual ApplicationRole? Role { get; set; }
    }
}
