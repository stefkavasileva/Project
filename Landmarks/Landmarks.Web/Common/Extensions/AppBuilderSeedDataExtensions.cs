using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Landmarks.Models;
using System.Threading.Tasks;
using Landmarks.Data;
using Microsoft.Extensions.DependencyInjection;
using Landmarks.Web.Common.Constants;

namespace Landmarks.Web.Common.Extensions
{
    public static class AppBuilderSeedDataExtensions
    {
        private static readonly IdentityRole[] roles =
        {
            new IdentityRole (SeedDbConstants.FullAdminRoleName),
            new IdentityRole (SeedDbConstants.FullOperatorRoleName),
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
                await CreateOperator(userManager);
            }
        }

        private static async Task CreateAdmin(UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync(SeedDbConstants.AdminEmail);

            if (user == null)
            {
                user = new User { UserName = SeedDbConstants.AdminEmail, Email = SeedDbConstants.AdminEmail };

                await userManager.CreateAsync(user, SeedDbConstants.DefaultAdminPassword);
                await userManager.AddToRoleAsync(user, roles[0].Name);
            }
        }

        private static async Task CreateOperator(UserManager<User> userManager)
        {
            var dbOperator = await userManager.FindByNameAsync(SeedDbConstants.OperatorEmail);

            if (dbOperator == null)
            {
                dbOperator = new User { UserName = SeedDbConstants.OperatorEmail, Email = SeedDbConstants.OperatorEmail };

                await userManager.CreateAsync(dbOperator, SeedDbConstants.DefaultOperatorPassword);
                await userManager.AddToRoleAsync(dbOperator, roles[1].Name);
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
