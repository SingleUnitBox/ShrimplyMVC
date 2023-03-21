using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShrimplyMVC.Models;
using ShrimplyMVC.Repositories;

namespace ShrimplyMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model = new UserIndexViewModel();
            var users = await _userRepository.GetAllAsync();
            if (users != null)
            {
                model.Users = users;
                return View(model);
            }    
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AddUser addUser)
        {
            var user = new IdentityUser
            {
                UserName = addUser.Username,
                Email = addUser.Email,
            };
            var roles = new List<string> { "User" };
            if (addUser.AdminCheckbox)
            {
                roles.Add("Admin");
            }
            var result = await _userRepository.AddAsync(user, addUser.Password, roles);

            return RedirectToAction("Index");
        }
    }
}
