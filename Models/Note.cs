/*namespace PixNote.Models
{

  public class Note
    {
        public int NoteId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        // Initialize the collection to avoid null reference issues
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
*/