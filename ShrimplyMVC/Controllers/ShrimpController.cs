using Microsoft.AspNetCore.Mvc;
using ShrimplyMVC.Data;
using ShrimplyMVC.Models;
using ShrimplyMVC.Models.Domain;
using ShrimplyMVC.Repositories;
using System.Text.Json;

namespace ShrimplyMVC.Controllers
{
    public class ShrimpController : Controller
    {
        private readonly IShrimpRepository _shrimpRepository;

        public ShrimpController(IShrimpRepository shrimpRepository)
        {
            _shrimpRepository = shrimpRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }
            
            var shrimps = await _shrimpRepository.GetAllAsync();
            return View(shrimps);
        }

        [HttpGet]
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

            var notification = new Notification
            {
                Message = "Shrimp created successfully.",
                Type = Enums.NotificationType.Success
            };
            TempData["Notification"] = JsonSerializer.Serialize(notification);

            await _shrimpRepository.CreateAsync(shrimp);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var existingShrimp = await _shrimpRepository.GetAsync(id);
            if (existingShrimp == null)
            {
                return NotFound();
            }
            return View(existingShrimp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Shrimp shrimp)
        {
            await _shrimpRepository.UpdateAsync(shrimp);

            ViewData["Notification"] = new Notification
            {
                Message = "Shrimp updated successfully.",
                Type = Enums.NotificationType.Success
            };

            return View();
        }
    }
}
