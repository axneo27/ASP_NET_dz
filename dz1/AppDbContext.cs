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
                .HasDefaultValue(0);
                p.Property(p => p.Amount)
                .HasDefaultValue(0);

                // Зв'язок один-до-багатьох між Product і Category
                p.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
