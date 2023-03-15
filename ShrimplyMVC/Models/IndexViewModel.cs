using ShrimplyMVC.Models.Domain;

namespace ShrimplyMVC.Models
{
    public class IndexViewModel
    {
        public ICollection<Shrimp> Shrimps { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
