using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PixNote.Models;

namespace PixNote.Models
{
    public class PhotoDbContext : IdentityDbContext<User>
    {
        public PhotoDbContext(DbContextOptions<PhotoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            modelBuilder.Entity<Image>()
                .HasMany(i => i.Comments)
                .WithOne(c => c.Image)
                .HasForeignKey(c => c.ImageId)
                .OnDelete(DeleteBehavior.Cascade);

             modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany()  // Assuming a one-to-many relationship
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.SetNull); // or another d

            modelBuilder.Entity<Image>()
                .HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId);
        }
    }
}
