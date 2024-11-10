using System;
using Microsoft.AspNetCore.Mvc; // For Controller and IActionResult
using System.ComponentModel.DataAnnotations; // For Required and StringLength attributes

namespace PixNote.Models

{
   public class Comment
    {
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public DateTime CommentDate { get; set; }

        // Foreign key relationships
        public int UserId { get; set; }
        public User? User { get; set; }
        public int ImageId { get; set; }
        public Image? Image { get; set; }
    }

}
