using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using PixNote.Models;
using System.IO;
using System.Threading.Tasks;

public class ImageService
{
    private readonly PhotoDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IWebHostEnvironment _env;

    public ImageService(PhotoDbContext context, UserManager<User> userManager, IWebHostEnvironment env)
    {
        _context = context;
        _userManager = userManager;
        _env = env;
    }

    public async Task<string> UploadImageAsync(IFormFile file, string userId)
    {
        string filePath = Path.Combine(_env.WebRootPath, "images", file.FileName);

        // Ensure the file is uploaded to the wwwroot/images directory
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var image = new Image
        {
            Title = file.FileName, // Assuming title can be the file name
            Description = "Image uploaded", // Modify this logic as necessary
            ImagePath = $"/images/{file.FileName}",
            DateUploaded = DateTime.Now,
            UserId = userId
        };

        _context.Images.Add(image);
        await _context.SaveChangesAsync();

        return image.ImagePath;
    }

    public async Task<bool> DeleteImageAsync(int imageId)
    {
        var image = await _context.Images.FindAsync(imageId);
        if (image == null) return false;

        // Delete image file from disk
        var filePath = Path.Combine(_env.WebRootPath, "images", Path.GetFileName(image.ImagePath));
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        _context.Images.Remove(image);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateImageAsync(int imageId, string title, string description)
    {
        var image = await _context.Images.FindAsync(imageId);
        if (image == null) return false;

        image.Title = title;
        image.Description = description;

        await _context.SaveChangesAsync();
        return true;
    }
}
