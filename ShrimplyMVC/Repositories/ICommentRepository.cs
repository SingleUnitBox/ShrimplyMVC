using ShrimplyMVC.Models.Domain;

namespace ShrimplyMVC.Repositories
{
    public interface ICommentRepository
    {
        Task<Comment> AddAsync(Comment comment);
        Task<IEnumerable<Comment>> GetAllComments(Guid shrimpId);
    }
}
