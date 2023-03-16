using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShrimplyMVC.Models;
using ShrimplyMVC.Repositories;
using System.Diagnostics;
using System.Text.Json;

namespace ShrimplyMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShrimpRepository _shrimpRepository;
        private readonly ITagRepository _tagRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger,
            IShrimpRepository shrimpRepository,
            ITagRepository tagRepository,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _shrimpRepository = shrimpRepository;
            _tagRepository = tagRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }
            var model = new IndexViewModel
            {
                Shrimps = (await _shrimpRepository.GetAllAsync()).ToList(),
                Tags = (await _tagRepository.GetAllAsync()).ToList()
            };

            return View(model);
        }

        [HttpGet]
        [Route("Shrimp/Tag/{tagName}")]
        public async Task<IActionResult> Tag(string tagName)
        {
            var shrimps = (await _shrimpRepository.GetAllAsync(tagName)).ToList();
            ViewData["tag"] = tagName;
            return View(shrimps);
        }

        [HttpGet]
        [Route("Shrimp/Details/{urlHandle}")]
        public async Task<IActionResult> Details(string urlHandle)
        {
            var shrimp = await _shrimpRepository.GetAsync(urlHandle);
            return View(shrimp);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser user)
        {
            var newUser = new IdentityUser
            {
                UserName = user.Username,
                Email = user.Email,
            };
            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded)
            {
                var addRoleResult = await _userManager.AddToRoleAsync(newUser, "User");
                if (addRoleResult.Succeeded)
                {
                    var notification = new Notification
                    {
                        Message = "User successfully registered.",
                        Type = Enums.NotificationType.Success
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                    return RedirectToAction("Index");
                }
            }
            ViewData["Notification"] = new Notification
            {
                Message = "Something went wrong.",
                Type = Enums.NotificationType.Error
            };
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}