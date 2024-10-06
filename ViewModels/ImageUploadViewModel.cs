using System.ComponentModel.DataAnnotations;

namespace PixNote.ViewModels
{
    public class ImageUploadViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select an image file")]
        public IFormFile ImageFile { get; set; }
    }
}
