using ShrimplyMVC.Models.Domain;

namespace ShrimplyMVC.Repositories
{
    public interface IShrimpRepository
    {
        IEnumerable<Shrimp> GetAllAsync();
    }
}
