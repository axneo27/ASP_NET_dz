using Microsoft.AspNetCore.Identity;

namespace spr421_spotify_clone.DAL.Entities.Identity
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public virtual ApplicationUser? User { get; set; }
    }
}
