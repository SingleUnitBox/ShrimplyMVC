using Microsoft.AspNetCore.Mvc;
using ShrimplyMVC.Data;
using ShrimplyMVC.Migrations;
using ShrimplyMVC.Models;
using ShrimplyMVC.Models.Domain;
using ShrimplyMVC.Repositories;
using System.Diagnostics;
using System.Text.Json;

namespace ShrimplyMVC.Controllers
{
    public class ShrimpController : Controller
    {
        private readonly IShrimpRepository _shrimpRepository;
        private readonly ITagRepository _tagRepository;

        public ShrimpController(IShrimpRepository shrimpRepository,
            ITagRepository tagRepository)
        {
            _shrimpRepository = shrimpRepository;
            _tagRepository = tagRepository;
        }
        [HttpGet]
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateShrimpViewModel createShrimp)
        {
            var tagsList = new List<Tag>();
            if (createShrimp.TagsString != null)
            {
                var tags = createShrimp.TagsString.Split(',');
                foreach (var tag in tags)
                {
                    var newTag = new Tag
                    {
                        Name = tag.Trim(),
                    };
                    tagsList.Add(newTag);
                }
            }

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
                Tags = tagsList,
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
            if (existingShrimp != null)
            {
                var shrimp = new EditShrimpViewModel
                {
                    Id = existingShrimp.Id,
                    Name = existingShrimp.Name,
                    Description = existingShrimp.Description,
                    Color = existingShrimp.Color,
                    Family = existingShrimp.Family,
                    FeaturedImageUrl = existingShrimp.FeaturedImageUrl,
                    UrlHandle = existingShrimp.UrlHandle,
                    PublishedDate = existingShrimp.PublishedDate,
                    Author = existingShrimp.Author,
                    IsVisible = existingShrimp.IsVisible,
                    TagsString = string.Join(',', existingShrimp.Tags.Select(x => x.Name.ToLower()))
                };
                return View(shrimp);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditShrimpViewModel existingShrimp)
        {
            var shrimp = new Shrimp
            {
                Id = existingShrimp.Id,
                Name = existingShrimp.Name,
                Description = existingShrimp.Description,
                Color = existingShrimp.Color,
                Family = existingShrimp.Family,
                FeaturedImageUrl = existingShrimp.FeaturedImageUrl,
                UrlHandle = existingShrimp.UrlHandle,
                PublishedDate = existingShrimp.PublishedDate,
                Author = existingShrimp.Author,
                IsVisible = existingShrimp.IsVisible,
                Tags = new List<Tag>(existingShrimp.TagsString.Split(',').Select(x => new Tag() { Name = x.Trim() }))
            };
            await _shrimpRepository.UpdateAsync(shrimp);

            ViewData["Notification"] = new Notification
            {
                Message = "Shrimp updated successfully.",
                Type = Enums.NotificationType.Success
            };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _shrimpRepository.DeleteAsync(id);
            var notification = new Notification
            {
                Message = "Shrimp deleted successfully.",
                Type = Enums.NotificationType.Success
            };
            TempData["Notification"] = JsonSerializer.Serialize(notification);
            return RedirectToAction("Index");
        }
    }
}
