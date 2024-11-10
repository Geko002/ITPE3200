using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PixNote.Models
{
 public class Image
    {
        public int ImageId { get; set; }
        
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        public string? ImagePath { get; set; }
        public DateTime DateUploaded { get; set; }

    [NotMapped]
       public IFormFile? imageFile {get; set;}

        // Relation with user in a foreign key settlement
        public int UserId { get; set; } 
        public User? User { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

