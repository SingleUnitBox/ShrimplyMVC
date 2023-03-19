using ShrimplyMVC.Models.Domain;

namespace ShrimplyMVC.Repositories
{
    public interface IShrimpLikeRepository
    {
        Task<int> GetTotalShrimpLikes(Guid shrimpId);
        Task AddShrimpLike(Guid shrimpId, Guid userId);
        Task<IEnumerable<ShrimpLike>> GetShrimpLikeUsers(Guid shrimpId);
    }
}
