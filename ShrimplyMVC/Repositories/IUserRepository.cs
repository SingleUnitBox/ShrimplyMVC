using Microsoft.AspNetCore.Identity;
using ShrimplyMVC.Models;
using ShrimplyMVC.Models.Domain;

namespace ShrimplyMVC.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAllAsync();
        Task<bool> AddAsync(IdentityUser identityUser, string password, List<string> roles);
        Task DeleteAsync(Guid id);
    }
}
