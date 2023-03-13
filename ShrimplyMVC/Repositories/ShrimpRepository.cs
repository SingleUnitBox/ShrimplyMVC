using Microsoft.EntityFrameworkCore;
using ShrimplyMVC.Data;
using ShrimplyMVC.Models.Domain;
using System.Runtime.CompilerServices;

namespace ShrimplyMVC.Repositories
{
    public class ShrimpRepository : IShrimpRepository
    {
        private readonly ShrimplyDbContext _shrimplyDbContext;

        public ShrimpRepository(ShrimplyDbContext shrimplyDbContext)
        {
            _shrimplyDbContext = shrimplyDbContext;
        }

        public async Task Create(Shrimp shrimp)
        {
            await _shrimplyDbContext.Shrimps.AddAsync(shrimp);
            await _shrimplyDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Shrimp>> GetAllAsync()
        {
            return await _shrimplyDbContext.Shrimps.ToListAsync();
        }

    }
}
