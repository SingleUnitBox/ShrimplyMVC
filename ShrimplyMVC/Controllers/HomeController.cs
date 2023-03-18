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
        private readonly IShrimpLikeRepository _shrimpLikeRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger,
            IShrimpRepository shrimpRepository,
            ITagRepository tagRepository,
            IShrimpLikeRepository shrimpLikeRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _shrimpRepository = shrimpRepository;
            _tagRepository = tagRepository;
            _shrimpLikeRepository = shrimpLikeRepository;
            _userManager = userManager;
            _signInManager = signInManager;
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
            if (shrimp != null)
            {
                ViewData["TotalLikes"] = await _shrimpLikeRepository.GetTotalShrimpLikes(shrimp.Id);
            }
            return View(shrimp);
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            var result = await _signInManager.PasswordSignInAsync(
                loginUser.Username, loginUser.Password, false, false);
            if (result.Succeeded)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "User logged in",
                    Type = Enums.NotificationType.Success
                };
                return RedirectToAction("Index");
            }

            ViewData["Notification"] = new Notification
            {
                Message = "Unable to login.",
                Type = Enums.NotificationType.Error
            };
            return View();
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
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            var notification = new Notification
            {
                Message = "User successfully logged out.",
                Type = Enums.NotificationType.Success
            };
            TempData["Notification"] = JsonSerializer.Serialize(notification);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AccessDenied()
        {
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