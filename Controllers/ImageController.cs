using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PixNote.Models;
using PixNote.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

 [HttpPost]
public async Task<IActionResult> Delete(int id)
{
    var image = await _photoDbContext.Images.FindAsync(id);
    
    if (image == null)
        return NotFound();

    _photoDbContext.Images.Remove(image);
    await _photoDbContext.SaveChangesAsync();

    TempData["Message"] = "Image deleted successfully!";
    return RedirectToAction("Index");
}


    // GET: Edit
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var image = _photoDbContext.Images.FirstOrDefault(i => i.ImageId == id);
        
        if (image == null)
            return NotFound();

        var viewModel = new ImageEditViewModel
        {
            ImageId = image.ImageId,
            Title = image.Title,
            Description = image.Description,
            ImagePath = image.ImagePath
        };

        return View(viewModel);
    }

    // POST: Edit
    [HttpPost]
    public async Task<IActionResult> Edit(ImageEditViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var image = await _photoDbContext.Images.FindAsync(model.ImageId);
        
        if (image == null)
            return NotFound();

        image.Title = model.Title;
        image.Description = model.Description;

        // Hvis du vil oppdatere bildet, må du håndtere filopplastingen her
        // Om ikke, behold den eksisterende ImagePath

        await _photoDbContext.SaveChangesAsync();
        return RedirectToAction("Details", new { id = model.ImageId });
    }
}
