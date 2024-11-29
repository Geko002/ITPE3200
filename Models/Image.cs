using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PixNote.Data;
namespace PixNote.Models
{
  public class Image
{
    public int ImageId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public DateTime DateUploaded { get; set; }
    public string UserId { get; set; } // Foreign key to User
     public ICollection<Comment> Comments { get; set; }
    public virtual User User { get; set; } // Navigation property
}
}
