using Microsoft.EntityFrameworkCore;
using ShrimplyMVC.Data;
using ShrimplyMVC.Models.Domain;

namespace ShrimplyMVC.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ShrimplyDbContext _shrimplyDbContext;

        public TagRepository(ShrimplyDbContext shrimplyDbContext)
        {
            _shrimplyDbContext = shrimplyDbContext;
        }
        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags = await _shrimplyDbContext.Tags.ToListAsync();
            return tags.DistinctBy(x => x.Name.ToLower());
        }
    }
}
