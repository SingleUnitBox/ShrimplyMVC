using ShrimplyMVC.Models.Domain;
using System.Runtime.InteropServices;

namespace ShrimplyMVC.Repositories
{
    public interface IShrimpRepository
    {
        Task<IEnumerable<Shrimp>> GetAllAsync();
        Task CreateAsync(Shrimp shrimp);
        Task<Shrimp> UpdateAsync(Shrimp shrimp);
        Task<Shrimp> GetAsync(Guid id);
    }
}
