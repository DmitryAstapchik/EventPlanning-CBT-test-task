using Microsoft.AspNetCore.Identity;

namespace EventPlanning.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            context.Database.EnsureCreated();

            var password = "Abc-123";
            var adminUserName = "admin@test.com";
            var adminRoleName = "Admin";

            if (await roleManager.FindByNameAsync(adminRoleName) == null)
            {
                var adminRole = new IdentityRole(adminRoleName);
                if (await roleManager.CreateAsync(adminRole) != IdentityResult.Success)
                {
                    throw new ApplicationException();
                }
            }

            var admin = await userManager.FindByNameAsync(adminUserName);
            if (admin == null)
            {
                admin = new IdentityUser(adminUserName);
                if (await userManager.CreateAsync(admin, password) != IdentityResult.Success)
                {
                    throw new ApplicationException();
                }
            }
            if (!await userManager.IsInRoleAsync(admin, adminRoleName))
            {
                if (await userManager.AddToRoleAsync(admin, adminRoleName) != IdentityResult.Success)
                {
                    throw new ApplicationException();
                }
            }
        }
    }
}
