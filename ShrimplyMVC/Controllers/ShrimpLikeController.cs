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
        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody] AddShrimpLike addShrimpLike)
        {
            await _shrimpLikeRepository.AddShrimpLike(addShrimpLike.ShrimpId, addShrimpLike.UserId);
            return Ok();
        }
        [Route("{shrimpId:Guid}/TotalLikes")]
        [HttpGet]
        public async Task<IActionResult> GetTotalShrimpLikes([FromRoute] Guid shrimpId)
        {
            var totalLikes = await _shrimpLikeRepository.GetTotalShrimpLikes(shrimpId);
            return Ok(totalLikes);
        }
    }
}
