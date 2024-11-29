using PixNote.Models;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace PixNote.ViewModels;
public class ImageDetailsViewModel
{
    public IEnumerable<Image> Images { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
    public IEnumerable<User> Users { get; set; }

    // New property to store user information for each image
    public Dictionary<int, string> ImageUploaderNames { get; set; }

    public ImageDetailsViewModel(IEnumerable<Image> images, IEnumerable<Comment> comments, IEnumerable<User> users)
    {
        Images = images;
        Comments = comments;
        Users = users;

        // Map image to uploader names (for each image, we store the uploader's username)
        ImageUploaderNames = images.ToDictionary(
            image => image.ImageId,
            image => users.FirstOrDefault(u => u.Id == image.UserId)?.UserName ?? "Unknown"
        );
    }
}

