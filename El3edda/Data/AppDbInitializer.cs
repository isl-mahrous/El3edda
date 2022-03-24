using El3edda.Data.Enums;
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
                if (!context.Mobiles.Any())
                {
                   context.Mobiles.AddRange(new List<Mobile>() 
                   {
                       //Add Mobiles Data
                       new Mobile()
                       {
                           Name = "Samsung Galaxy S10",
                            ReleaseDate = new DateTime(2020,1,1),
                            Price = 899.99,
                            Description = "The Samsung Galaxy S10 is a smartphone designed and marketed by Samsung Electronics, a division of Samsung Corporation. It was released on September 3, 2020, by Samsung Electronics in the United States and on March 31, 2020, in Europe. It was the tenth-generation of the Galaxy S series of smartphones, and the first smartphone to feature a notch.",
                            WarrantyPeriod = new TimeSpan(0,0,12,0),
                            UnitsInStock = 10,
                            MainPhotoURL = "https://mobizil.com/wp-content/uploads/2019/02/samsung-s10.jpg",
                            ManufacturerId = 1,
                            Media = new List<Media>(){
                                new Media(){
                                    URL = "https://www.youtube.com/watch?v=dQw4w9WgXcQ", 
                                    Type = MediaType.Video}
                            },
                            Specs = new Specs(){
                                CPU = "Snapdragon 855",
                                Screen = ScreenEnum.AMOLED,
                                Height = 6.5,
                                Width = 6.5,
                                Thickness = 0.3,
                                CameraMegaPixels = 16,
                                Color = Colors.Black,
                                Weight = 150,
                                OS = OSEnum.Android,
                                BatteryCapacity = 4000
                            }
                       }
                    //    new Mobile()
                    //    {

                    //    },
                    //    new Mobile()
                    //    {

                    //    },
                    //    new Mobile()
                    //    {

                    //    }

                   });
                   context.SaveChanges();
                }

                




            }

        }

    }
}
