using Microsoft.AspNetCore.Mvc;
using ShrimplyMVC.Data;
using ShrimplyMVC.Models;
using ShrimplyMVC.Models.Domain;
using ShrimplyMVC.Repositories;

namespace ShrimplyMVC.Controllers
{
    public class ShrimpController : Controller
    {
        private readonly IShrimpRepository _shrimpRepository;

        public ShrimpController(IShrimpRepository shrimpRepository)
        {
            _shrimpRepository = shrimpRepository;
        }
        public async Task<IActionResult> Index()
        {
            var shrimps = await _shrimpRepository.GetAllAsync();
            return View(shrimps);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateShrimpViewModel createShrimp)
        {
            var shrimp = new Shrimp
            {
                Id = Guid.NewGuid(),
                Name = createShrimp.Name,
                Description = createShrimp.Description,
                Color = createShrimp.Color,
                Family = createShrimp.Family,
                FeaturedImageUrl = createShrimp.FeaturedImageUrl,
                UrlHandle = createShrimp.UrlHandle,
                PublishedDate = createShrimp.PublishedDate,
                Author = createShrimp.Author,
                IsVisible = createShrimp.IsVisible,
            };
            await _shrimpRepository.Create(shrimp);
            return RedirectToAction("Index");
        }
    }
}
