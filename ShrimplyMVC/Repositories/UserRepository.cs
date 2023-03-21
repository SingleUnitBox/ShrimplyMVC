using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShrimplyMVC.Data;
using ShrimplyMVC.Models;

namespace ShrimplyMVC.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _authDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(AuthDbContext authDbContext,
            UserManager<IdentityUser> userManager)
        {
            _authDbContext = authDbContext;
            _userManager = userManager;
        }

        public async Task<bool> AddAsync(IdentityUser identityUser, string password, List<string> roles)
        {
            var result = await _userManager.CreateAsync(identityUser, password);
            if (result.Succeeded)
            {
                var rolesResult = await _userManager.AddToRolesAsync(identityUser, roles);
                if (rolesResult.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null) 
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<IEnumerable<IdentityUser>> GetAllAsync()
        {
            var users = await _authDbContext.Users.ToListAsync();
            var superAdminUser = await _authDbContext.Users.FirstOrDefaultAsync(
                x => x.Email == "superadmin@shrimply.com");
            if (superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }
            return users;
        }

    }
}
