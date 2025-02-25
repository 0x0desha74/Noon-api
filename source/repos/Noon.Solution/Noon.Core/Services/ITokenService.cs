using Microsoft.AspNetCore.Identity;
using Noon.Core.Entities.Identity;

namespace Noon.Core.Services
{
    public interface ITokenService
    {
        public Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);

    }
}
