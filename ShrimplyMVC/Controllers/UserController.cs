using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShrimplyMVC.Models;
using ShrimplyMVC.Repositories;
using System.Text.Json;

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
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

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
            var notification = new Notification
            {
                Message = "User successfully registered.",
                Type = Enums.NotificationType.Success
            };
            TempData["Notification"] = JsonSerializer.Serialize(notification);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
