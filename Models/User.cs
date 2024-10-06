using System;

namespace PixNote.Models

{

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string ?Email { get; set; }
    public ICollection<Image> ?Images { get; set; }
    public ICollection<Comment> ?Comments { get; set; }
    public ICollection<Note> Notes { get; set; }
}
}
