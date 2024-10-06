using System;

namespace PixNote.Models
{
 public class Image
{
    public int ImageId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public DateTime DateUploaded { get; set; }

    // Relation with user in a foreign key settlement
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<Comment> Comments { get; set; } // this will inherit the Comments as collection were it can be edited. 
}
}

