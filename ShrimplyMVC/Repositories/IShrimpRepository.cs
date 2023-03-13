using ShrimplyMVC.Models.Domain;
using System.Runtime.InteropServices;

namespace ShrimplyMVC.Repositories
{
    public interface IShrimpRepository
    {
        Task<IEnumerable<Shrimp>> GetAllAsync();
        Task Create(Shrimp shrimp);
    }
}
