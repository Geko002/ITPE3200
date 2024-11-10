using System;

namespace PixNote.Models

{

 public class User
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

        // Initialize collections to avoid null reference issues
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
       // public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
