using ShrimplyMVC.Models.Domain;

namespace ShrimplyMVC.Models
{
    public class DetailsViewModel
    {
        public Shrimp Shrimp { get; set; }
        public AddComment AddComment { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; }
        public DetailsViewModel()
        {
            AddComment = new AddComment();
        }
    }
}
