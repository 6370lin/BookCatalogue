using ApplicationCore.Constants;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //await roleManager.CreateAsync(new IdentityRole(BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS));
            string defaultUserName = "demouser@microsoft.com";
            var defaultUser = new ApplicationUser { UserName = defaultUserName, Email = defaultUserName };
            await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);
            defaultUser = await userManager.FindByNameAsync(defaultUserName);
            var email_confirmation_token = await userManager.GenerateEmailConfirmationTokenAsync(defaultUser);
            await userManager.ConfirmEmailAsync(defaultUser, email_confirmation_token);

            string adminUserName = "admin@microsoft.com";
            var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };
            await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
            adminUser = await userManager.FindByNameAsync(adminUserName);
            var admin_email_confirmation_token = await userManager.GenerateEmailConfirmationTokenAsync(adminUser);
            await userManager.ConfirmEmailAsync(adminUser, admin_email_confirmation_token);
            //await userManager.AddToRoleAsync(adminUser, BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS);
        }
    }
}
