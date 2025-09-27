using Microsoft.EntityFrameworkCore;
using dz1.Models;

namespace dz1
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(c =>
            {
                c.HasKey(c => c.Id);
                c.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(p =>
            {
                p.HasKey(p => p.Id);
                p.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
                p.Property(p => p.Description)
                .HasMaxLength(500);
                p.Property(p => p.Price)
                .HasPrecision(18, 2)
                .HasDefaultValue(0m);
                p.Property(p => p.Amount)
                .HasDefaultValue(0);

                // Зв'язок один-до-багатьох між Product і Category
                p.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(u => u.Id);
                u.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
                u.Property(u => u.LastName).IsRequired().HasMaxLength(50);
                u.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                u.Property(u => u.Email).IsRequired();
                u.Property(u => u.Password).IsRequired();
                u.HasIndex(u => u.UserName).IsUnique();
                u.HasIndex(u => u.Email).IsUnique();
            });
        }
    }
}
