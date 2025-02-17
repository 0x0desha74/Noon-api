using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Noon.Core.Entities.Identity;
using System.Security.Claims;

namespace Noon.API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser?> FindUserAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == email);

            return user;
        }
    }
}
