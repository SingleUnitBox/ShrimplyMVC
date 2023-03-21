using Microsoft.AspNetCore.Identity;

namespace ShrimplyMVC.Models
{
    public class UserIndexViewModel
    {
        public UserIndexViewModel()
        {
            AddUser = new AddUser();
        }
        public AddUser AddUser { get; set; }
        public IEnumerable<IdentityUser> Users { get; set; }
    }
}
