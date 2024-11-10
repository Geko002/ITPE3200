using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations.Schema;
namespace PixNote.ViewModels
{
    public class ImageUploadViewModel
    {

        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public int UserId { get; set; } // UserId of the uploader
        public string? ImagePath { get; set; }     // Store the path for uploaded image
        public DateTime DateUploaded { get; set; }
        [Required]
        public IFormFile? imageFile { get; set; } // For file upload
    }
}
