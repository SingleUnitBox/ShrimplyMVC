namespace ShrimplyMVC.Models.Domain
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid ShrimpId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
