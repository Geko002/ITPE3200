using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PixNote.ViewModels;
public class ImageEditViewModel
{
    public int ImageId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }

    [Display(Name = "New Image")]
    public IFormFile NewImageFile { get; set; }  // For updating the image
}
