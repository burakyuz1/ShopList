using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public static class IdentityShopListDbContextSeed
    {
        public async static Task SeedAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            await roleManager.CreateAsync(new IdentityRole("admin"));

            string adminMail = "admin@example.com";
            var adminUser = new ApplicationUser() { UserName = adminMail, Email = adminMail, EmailConfirmed = true };
            await userManager.CreateAsync(adminUser,"Ankara1.");
            await userManager.AddToRoleAsync(adminUser, "admin");
        }
    }
}
