using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Models
{
    public static class SeedData
    {
        public static async Task InitializeRoles(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminEmail = "admin@gmail.com"; // Replace with desired admin email
            var adminPassword = "Admin@123"; // Replace with desired admin password
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
