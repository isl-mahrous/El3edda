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
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Manufacturers
                if (!context.Manufacturers.Any())
                {
                    context.Manufacturers.AddRange(new List<Manufacturer>()
                    {
                        //Add Manufacturers Data
                        new Manufacturer(){ Name = "Samsung", Origin="Korea"}, //ID=1
                        new Manufacturer(){ Name = "Apple", Origin="USA"}, // ID=2
                        new Manufacturer(){ Name = "Xiaomi", Origin="China"},
                        new Manufacturer(){ Name = "Huawei", Origin="China"},
                        new Manufacturer(){ Name = "Nokia", Origin="Finland"},
                        new Manufacturer(){ Name = "OnePlus", Origin="China"},
                        new Manufacturer(){ Name = "Oppo", Origin="China"},
                        new Manufacturer(){ Name = "Vivo", Origin="China"},
                        new Manufacturer(){ Name = "Other", Origin="Unknown"}
                    });
                    context.SaveChanges();
                }

                //Mobiles
                //if (!context.Mobiles.Any())
                //{
                //    context.Mobiles.AddRange(new List<Mobile>() 
                //    {
                //        //Add Mobiles Data
                //        new Mobile()
                //        {

                //        },
                //        new Mobile()
                //        {

                //        },
                //        new Mobile()
                //        {

                //        },
                //        new Mobile()
                //        {

                //        }

                //    });
                //    context.SaveChanges();
                //}

                




            }

        }

    }
}
