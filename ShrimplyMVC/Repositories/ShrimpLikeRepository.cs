using Microsoft.EntityFrameworkCore;
using ShrimplyMVC.Data;
using ShrimplyMVC.Models.Domain;

namespace ShrimplyMVC.Repositories
{
    public class ShrimpLikeRepository : IShrimpLikeRepository
    {
        private readonly ShrimplyDbContext _shrimplyDbContext;

        public ShrimpLikeRepository(ShrimplyDbContext shrimplyDbContext)
        {
            _shrimplyDbContext = shrimplyDbContext;
        }

        public async Task AddShrimpLike(Guid shrimpId, Guid userId)
        {
            var like = new ShrimpLike
            {
                Id = Guid.NewGuid(),
                ShrimpId = shrimpId,
                UserId = userId,
            };
            await _shrimplyDbContext.Likes.AddAsync(like);
            await _shrimplyDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShrimpLike>> GetAllLikes(Guid shrimpId)
        {
            var likes = await _shrimplyDbContext.Likes.Where(x => x.ShrimpId == shrimpId)
                .ToListAsync();
            return likes;

        }

        public async Task<int> GetTotalShrimpLikes(Guid shrimpId)
        {
            var likes = (await _shrimplyDbContext.Likes.ToListAsync())
                .Where(x => x.ShrimpId == shrimpId).Count();
            return likes;
        }
    }
}
