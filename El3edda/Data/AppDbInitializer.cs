using El3edda.Data.Static;
using El3edda.Models;
using Microsoft.AspNetCore.Identity;

namespace El3edda.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedUsersAndRoles(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                //Roles
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                //Admin
                var adminEmail = "admin@el3edda.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin",
                        Email = adminEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@1234");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                //App User
                var userEmail = "user@el3edda.com";
                var appUser = await userManager.FindByEmailAsync(userEmail);

                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "App User",
                        UserName = "appUser",
                        Email = userEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "User@1234");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

            }
        }


    }
}
