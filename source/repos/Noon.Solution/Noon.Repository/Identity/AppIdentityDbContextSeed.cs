using Microsoft.AspNetCore.Identity;
using Noon.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Repository.Identity
{
  public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Mustafa Elsayed",
                    Email = "mustafa.elsayed@gmail.com",
                    UserName = "mustafa.elsayed",
                    PhoneNumber = "01223043905"
                };
                await userManager.CreateAsync(user, "P@ssword123");
            }
        }
    }
}
