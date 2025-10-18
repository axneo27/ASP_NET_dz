using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using spr421_spotify_clone.DAL.Entities.Identity;
using spr421_spotify_clone.DAL.Settings;

namespace spr421_spotify_clone.DAL.Initializer
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            if(!roleManager.Roles.Any())
            {
                var roleAdmin = new ApplicationRole { Name = RoleSettings.RoleAdmin };
                var roleUser = new ApplicationRole { Name = RoleSettings.RoleUser };

                await roleManager.CreateAsync(roleAdmin);
                await roleManager.CreateAsync(roleUser);
            }

            if(!userManager.Users.Any())
            {
                var admin = new ApplicationUser 
                {
                    Email = "admin@mail.com", 
                    UserName = "admin",
                    EmailConfirmed = true,
                    FirstName = "Spotify"
                };

                var user = new ApplicationUser
                {
                    Email = "user@mail.com",
                    UserName = "user",
                    EmailConfirmed = true,
                    FirstName = "John",
                    LastName = "Smith"
                };

                await userManager.CreateAsync(admin, "qwerty");
                await userManager.CreateAsync(user, "qwerty");

                await userManager.AddToRolesAsync(admin, [RoleSettings.RoleAdmin, RoleSettings.RoleUser]);
                await userManager.AddToRoleAsync(user, RoleSettings.RoleUser);
            }
        }
    }
}
