using Microsoft.AspNetCore.Mvc;
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


    public IActionResult Index(){
    var images = _photoDbContext.Images.ToList();
    var imageDetailsViewModel = new ImageDetailsViewModel(images, new List<Comment>());
    return View("ScrollView", imageDetailsViewModel); 
}


   
    public IActionResult Details(int id)
    {
   
        var image = _photoDbContext.Images.FirstOrDefault(i => i.ImageId == id);
        
        if (image == null)
            return NotFound();

      
        var comments = _photoDbContext.Comments.Where(c => c.ImageId == id).ToList();

        
        var viewModel = new ImageDetailsViewModel(new List<Image> { image }, comments);

        return View(viewModel);
    }
    [HttpPost]
    public async Task<IActionResult> Upload(ImageUploadViewModel model)
    {
        if (ModelState.IsValid)
        {
            string filePath = Path.Combine("wwwroot/images", model.ImageFile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(stream);
            }

        
            var image = new Image
            {
                Title = model.Title,
                Description = model.Description,
                ImagePath = $"/images/{model.ImageFile.FileName}",
                DateUploaded = DateTime.Now
            };

            _photoDbContext.Images.Add(image);
            await _photoDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        return View("Upload", model);
    }
}
