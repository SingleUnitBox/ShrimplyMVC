namespace ShrimplyMVC.Models
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
