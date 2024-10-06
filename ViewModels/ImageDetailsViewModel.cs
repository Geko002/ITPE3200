using PixNote.Models;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace PixNote.ViewModels;
public class ImageDetailsViewModel
{

    public IEnumerable<User> users {get; set;}
    public IEnumerable<Image> Image { get; set; } 
    public IEnumerable<Comment> Comments { get; set; } 



    public ImageDetailsViewModel(IEnumerable<Image> image, IEnumerable<Comment> comments)
    {
        Image = image;
        Comments = comments;
    }
}


