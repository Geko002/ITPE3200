using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PixNote.Models;
namespace PixNote.Data
{
    public static class DbInit
    {
        public static async Task Initialize(PhotoDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Ensure the database is created
            context.Database.Migrate();

            // Seed roles
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Seed a default admin user
            if (await userManager.FindByEmailAsync("admin@pixnote.com") == null)
            {
                var adminUser = new User
                {
                    Email = "admin@pixnote.com",
                };

                await userManager.CreateAsync(adminUser, "Admin123!");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Seed images and comments
            if (!context.Images.Any())
            {
                var user = await userManager.FindByEmailAsync("admin@pixnote.com");
                var image = new Image
                {
                    Title = "Sample Image",
                    Description = "A beautiful sample image",
                    ImagePath = "/uploads/sample.jpg",
                    DateUploaded = DateTime.Now,
                    UserId = user.Id
                };

                context.Images.Add(image);

                var comment = new Comment
                {
                    CommentText = "This is a sample comment.",
                    CommentDate = DateTime.Now,
                    UserId = user.Id,
                    Image = image
                };

                context.Comments.Add(comment);

                await context.SaveChangesAsync();
            }
        }
    }
}
