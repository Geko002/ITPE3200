using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;
using PixNote.Models;
using PixNote.ViewModels;
using System.Collections.Generic;
using System.Linq;
public class ImageController : Controller
{
    private readonly PhotoDbContext _photoDbContext;

    public ImageController(PhotoDbContext photoDbContext)
    {
        _photoDbContext = photoDbContext;
    }

    public IActionResult Index()
    {
        var images = _photoDbContext.Images.ToList();
        var comments = _photoDbContext.Comments.ToList();
        var users = _photoDbContext.Users.ToList();

        var imageDetailsViewModel = new ImageDetailsViewModel(images, comments, users);
        return View("ScrollView", imageDetailsViewModel); 
    }
   
    public IActionResult Details(int id)
{
    var image = _photoDbContext.Images.FirstOrDefault(i => i.ImageId == id);
    
    if (image == null)
        return NotFound();

    var comments = _photoDbContext.Comments.Where(c => c.ImageId == id).ToList();
    var users = _photoDbContext.Users.ToList();

    var viewModel = new ImageDetailsViewModel(new List<Image> { image }, comments, users);

    return View(viewModel);
}
[HttpGet]
public IActionResult Upload()
{
    return View(); 
}

[HttpPost]
public async Task<IActionResult> Upload(ImageUploadViewModel Mod)
{
    if (Mod.imageFile != null)
    {
        string filePath = Path.Combine("wwwroot/images", Mod.imageFile.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await Mod.imageFile.CopyToAsync(stream);
        }
        var image = new Image
        {
            Title = Mod.Title,
            Description = Mod.Description,
            ImagePath = $"/images/{Mod.imageFile.FileName}",
            DateUploaded = DateTime.Now,
            UserId = 1
        };

        _photoDbContext.Images.Add(image);
        await _photoDbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    return View("Upload", Mod);
}
}
