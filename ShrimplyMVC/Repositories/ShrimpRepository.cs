using Microsoft.EntityFrameworkCore;
using ShrimplyMVC.Data;
using ShrimplyMVC.Models.Domain;

namespace ShrimplyMVC.Repositories
{
    public class ShrimpRepository : IShrimpRepository
    {
        private readonly ShrimplyDbContext _shrimplyDbContext;

        public ShrimpRepository(ShrimplyDbContext shrimplyDbContext)
        {
            _shrimplyDbContext = shrimplyDbContext;
        }
        public IEnumerable<Shrimp> GetAllAsync()
        {
            return _shrimplyDbContext.Shrimps.ToList();
        }
    }
}
