using Microsoft.AspNetCore.Mvc;
using ShrimplyMVC.Models;
using ShrimplyMVC.Repositories;
using System.Diagnostics;

namespace ShrimplyMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShrimpRepository _shrimpRepository;
        private readonly ITagRepository _tagRepository;

        public HomeController(ILogger<HomeController> logger,
            IShrimpRepository shrimpRepository,
            ITagRepository tagRepository)
        {
            _logger = logger;
            _shrimpRepository = shrimpRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Shrimps = (await _shrimpRepository.GetAllAsync()).ToList(),
                Tags = (await _tagRepository.GetAllAsync()).ToList()
            };

            return View(model);
        }

        [HttpGet]
        [Route("tag/{tagName}")]
        public async Task<IActionResult> Tag(string tagName)
        {
            var shrimps = (await _shrimpRepository.GetAllAsync(tagName)).ToList();
            ViewData["tag"] = tagName;
            return View(shrimps);
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