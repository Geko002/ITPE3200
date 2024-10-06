using Microsoft.EntityFrameworkCore;
namespace PixNote.Models;

public class PhotoDbContext : DbContext
{
  public PhotoDbContext(DbContextOptions<PhotoDbContext> options) : base (options) {

    Database.EnsureCreated();

  }


public DbSet<User> Users {get; set;}
public DbSet<Image> Images {get; set;}
public DbSet<Comment> Comments {get; set;}

}
