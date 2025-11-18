using Microsoft.EntityFrameworkCore;
using ASP.NET_Core_REST_API.Models;

namespace ASP.NET_Core_REST_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<TaskItem> Tasks { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
            
                entity.HasOne(t => t.User).WithMany(u => u.Tasks).HasForeignKey(t => t.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity => {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");
            });
        }

    }

}