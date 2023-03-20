using Microsoft.EntityFrameworkCore;
using ShrimplyMVC.Data;
using ShrimplyMVC.Models.Domain;

namespace ShrimplyMVC.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ShrimplyDbContext _shrimplyDbContext;

        public CommentRepository(ShrimplyDbContext shrimplyDbContext)
        {
            _shrimplyDbContext = shrimplyDbContext;
        }
        public async Task<Comment> AddAsync(Comment comment)
        {
            await _shrimplyDbContext.Comments.AddAsync(comment);
            await _shrimplyDbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var comment = await _shrimplyDbContext.Comments.FindAsync(id);
            if (comment != null)
            {
                _shrimplyDbContext.Comments.Remove(comment);
                await _shrimplyDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Comment>> GetAllComments(Guid shrimpId)
        {
            var comments = await _shrimplyDbContext.Comments.Where(x => x.ShrimpId == shrimpId)
                .ToListAsync();
            return comments;
        }
    }
}
