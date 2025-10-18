using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using spr421_spotify_clone.DAL.Entities;
using spr421_spotify_clone.DAL.Entities.Identity;

namespace spr421_spotify_clone.DAL
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,
        string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
    {
        public AppDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<ArtistEntity> Artists { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<TrackEntity> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ArtistEntity
            builder.Entity<ArtistEntity>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);
            });

            // GenreEntity
            builder.Entity<GenreEntity>(e =>
            {
                e.HasKey(g => g.Id);
                e.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(50);
            });

            // TrackEntity
            builder.Entity<TrackEntity>(e =>
            {
                e.HasKey(t => t.Id);
                e.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);
                e.Property(t => t.AudioUrl)
                .IsRequired()
                .HasMaxLength(50);
            });

            // Relationships
            builder.Entity<TrackEntity>()
                .HasOne(t => t.Genre)
                .WithMany(g => g.Tracks)
                .HasForeignKey(t => t.GenreId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<TrackEntity>()
                .HasMany(t => t.Artists)
                .WithMany(a => a.Tracks)
                .UsingEntity("ArtistTracks");

            // Identity
            builder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });
        }
    }
}
