using Microsoft.AspNetCore.Identity;
using Noon.Core.Entities.Identity;
using Noon.Repository.Identity;

namespace Noon.API.Extensions
{
    public static class IdentityServicesExtensions
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options => {

            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddAuthentication();
            
            
            return services;
        }
    }
}
