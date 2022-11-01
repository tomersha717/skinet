using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Tomer",
                    Email = "tomersha717@gmail.com",
                    UserName = "tomersha717@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Tomer",
                        LastName = "Shalom",
                        Street = "Yaakov dori 5",
                        City = "Ramle",
                        State = "NY",
                        ZipCode = "90231"
                    }
                };

                await userManager.CreateAsync(user,"Pa$$w0rd");

                
            }
        }
    }
}