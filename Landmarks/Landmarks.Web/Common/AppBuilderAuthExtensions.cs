using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Landmarks.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Landmarks.Web.Common
{
    public static class AppBuilderAuthExtensions
    {
        private const string DefaultAdminPassword = "admin_pass_123";

        private static readonly IdentityRole[] roles =
        {
            new IdentityRole ("Administrator"),
            new IdentityRole ("Lecturer"),
        };

        public static async void SeedDatabase(this IApplicationBuilder builder)
        {
            var serviceFactory = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();

            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                await SeedIdentityRoles(roleManager);
                await CreateAdmin(userManager);
            }
        }

        private static async Task CreateAdmin(UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync("admin");

            if (user == null)
            {
                user = new User { UserName = "admin@example.com", Email = "admin@example.com" };

                await userManager.CreateAsync(user, DefaultAdminPassword);
                await userManager.AddToRoleAsync(user, roles[0].Name);
            }
        }

        private static async Task SeedIdentityRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var identityRole in roles)
            {
                if (!await roleManager.RoleExistsAsync(identityRole.Name))
                {
                    await roleManager.CreateAsync(identityRole);
                }
            }
        }
    }
}
