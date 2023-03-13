using Microsoft.AspNetCore.Mvc;
using ShrimplyMVC.Data;
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
            var shrimps = _shrimpRepository.GetAllAsync();
            return View(shrimps);
        }
    }
}
