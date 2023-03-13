namespace ShrimplyMVC.Models
{
    public class CreateShrimpViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Family { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }
    }
}
