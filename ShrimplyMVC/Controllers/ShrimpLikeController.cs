using Microsoft.AspNetCore.Mvc;
using ShrimplyMVC.Models;
using ShrimplyMVC.Repositories;

namespace ShrimplyMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShrimpLikeController : Controller
    {
        private readonly IShrimpLikeRepository _shrimpLikeRepository;

        public ShrimpLikeController(IShrimpLikeRepository shrimpLikeRepository)
        {
            _shrimpLikeRepository = shrimpLikeRepository;
        }
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddShrimpLike addShrimpLike)
        {
            await _shrimpLikeRepository.AddShrimpLike(addShrimpLike.ShrimpId, addShrimpLike.UserId);
            return Ok();
        }
    }
}
