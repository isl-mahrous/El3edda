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
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<
                    RoleManager<IdentityRole>
                >();

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
                var userManager = serviceScope.ServiceProvider.GetRequiredService<
                    UserManager<ApplicationUser>
                >();

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
                    context.Manufacturers.AddRange(
                        new List<Manufacturer>()
                        {
                            //Add Manufacturers Data
                            new Manufacturer() { Name = "Samsung", Origin = "Korea" }, //ID=1
                            new Manufacturer() { Name = "Apple", Origin = "USA" }, // ID=2
                            new Manufacturer() { Name = "Xiaomi", Origin = "China" },
                            new Manufacturer() { Name = "Huawei", Origin = "China" },
                            new Manufacturer() { Name = "Nokia", Origin = "Finland" },
                            new Manufacturer() { Name = "OnePlus", Origin = "China" },
                            new Manufacturer() { Name = "Oppo", Origin = "China" },
                            new Manufacturer() { Name = "Vivo", Origin = "China" },
                            new Manufacturer() { Name = "Other", Origin = "Unknown" }
                        }
                    );
                    context.SaveChanges();
                }

                //Mobiles
                if (!context.Mobiles.Any())
                {
                    context.Mobiles.AddRange(
                        new List<Mobile>()
                        {
                            //Add Mobiles Data
                            new Mobile()
                            {
                                Name = "Samsung Galaxy S10",
                                ReleaseDate = new DateTime(2020, 1, 1),
                                Price = 899.99,
                                Description =
                                    "The Samsung Galaxy S10 is a smartphone designed and marketed by Samsung Electronics, a division of Samsung Corporation. It was released on September 3, 2020, by Samsung Electronics in the United States and on March 31, 2020, in Europe. It was the tenth-generation of the Galaxy S series of smartphones, and the first smartphone to feature a notch.",
                                WarrantyPeriod = 12,
                                UnitsInStock = 10,
                                UnitsSold = 100,
                                MainPhotoURL =
                                    "https://mobizil.com/wp-content/uploads/2019/02/samsung-s10.jpg",
                                ManufacturerId = 1,
                                Media = new List<Media>()
                                {
                                    new Media()
                                    {
                                        URL =
                                            "https://www.theiphonewiki.com/w/images/1/17/IPhone_13_mini.png",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://www.theiphonewiki.com/w/images/1/17/IPhone_13_mini.png",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://m.media-amazon.com/images/I/71geVdy6-OS._SX569_.jpg",
                                        Type = MediaType.Photo
                                    }
                                },
                                Specs = new Specs()
                                {
                                    CPU = "Snapdragon 855",
                                    Screen = ScreenEnum.AMOLED,
                                    Height = 6.5,
                                    Width = 6.5,
                                    Thickness = 0.3,
                                    CameraMegaPixels = 16,
                                    Color = Colors.Black,
                                    Weight = 150,
                                    OS = OSEnum.Android,
                                    BatteryCapacity = 4000,
                                    RAM = 12,
                                    Storage = 128
                                }
                            },
                            new Mobile()
                            {
                                Name = "Xiaomi Redmi Note 8 Pro",
                                ReleaseDate = new DateTime(2020, 1, 1),
                                Price = 600,
                                Description =
                                    "The Xiaomi Redmi Note 8 Pro is a smartphone designed and marketed by Xiaomi Communications, a division of Xiaomi Group. It was released on September 3, 2020, by Xiaomi Communications in the United States and on March 31, 2020, in Europe.",
                                WarrantyPeriod = 5,
                                UnitsInStock = 10,
                                UnitsSold = 700,
                                MainPhotoURL =
                                    "https://mobizil.com/wp-content/uploads/2019/09/Xiaomi-Redmi-Note-8-Pro.jpg",
                                ManufacturerId = 3,
                                Media = new List<Media>()
                                {
                                    new Media()
                                    {
                                        URL =
                                            "https://itstechprice.com/wp-content/uploads/2020/10/vivo-phones.jpg",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://www.pricenow.com.pk/templates/yootheme/cache/Vivo_V21e-a-e37a8098.jpeg",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://www.mobiletech.ie/user/products/mobiletech-p40-silicon-clear1.jpg",
                                        Type = MediaType.Photo
                                    }
                                },
                                Specs = new Specs()
                                {
                                    CPU = "Snapdragon 855",
                                    Screen = ScreenEnum.IPS,
                                    Height = 6.5,
                                    Width = 6.5,
                                    Thickness = 0.3,
                                    CameraMegaPixels = 16,
                                    Color = Colors.Black,
                                    Weight = 150,
                                    OS = OSEnum.Android,
                                    BatteryCapacity = 4000,
                                    RAM = 12,
                                    Storage = 128
                                }
                            },
                            // Apple Mobiles
                            new Mobile()
                            {
                                Name = "iPhone 11",
                                ReleaseDate = new DateTime(2020, 1, 1),
                                Price = 650,
                                Description =
                                    "The iPhone 11 is a smartphone designed and marketed by Apple Inc. It was released on September 3, 2020, by Apple Inc. It was the tenth-generation of the iPhone series of smartphones, and the first smartphone to feature a notch.",
                                WarrantyPeriod = 12 * 2,
                                UnitsInStock = 10,
                                UnitsSold = 400,
                                MainPhotoURL =
                                    "https://mobizil.com/wp-content/uploads/2019/09/Apple-iPhone-11.jpg",
                                ManufacturerId = 2,
                                Media = new List<Media>()
                                {
                                    new Media()
                                    {
                                        URL =
                                            "https://www.mytrendyphone.eu/images/Original-Clear-Case-for-Huawei-P40-51993731-Transparent-6901443365999-10042020-01-p.jpg",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://www.theiphonewiki.com/w/images/1/17/IPhone_13_mini.png",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://www.pricenow.com.pk/templates/yootheme/cache/Vivo_V21e-a-e37a8098.jpeg",
                                        Type = MediaType.Photo
                                    }
                                },
                                Specs = new Specs()
                                {
                                    CPU = "Apple A13 Bionic",
                                    Screen = ScreenEnum.AMOLED,
                                    Height = 6.5,
                                    Width = 6.5,
                                    Thickness = 0.3,
                                    CameraMegaPixels = 16,
                                    Color = Colors.Red,
                                    Weight = 150,
                                    OS = OSEnum.Android,
                                    BatteryCapacity = 4000,
                                    RAM = 4,
                                    Storage = 64
                                }
                            },
                            new Mobile()
                            {
                                Name = "iPhone SE (3rd generation)",
                                ReleaseDate = new DateTime(2022, 3, 18),
                                Price = 479,
                                Description =
                                    "The iPhone SE 2022 is a phone that crams modern guts into a classic design that's so small it’s almost quaint. And despite the retro vibes the end result is pretty compelling for those on a budget. For $429, you get 5G connectivity and the same A15 Bionic processor that's in the iPhone 13, which means you'll enjoy best-in-class performance and great looking photos.",
                                WarrantyPeriod = 12 * 3,
                                UnitsInStock = 20,
                                MainPhotoURL =
                                    "https://oxygendigitalshop.com/media/cache/375x0/catalog/product/s/a/sad_1_1622666312.webp",
                                ManufacturerId = 2,
                                Media = new List<Media>()
                                {
                                    new Media()
                                    {
                                        URL =
                                            "https://oxygendigitalshop.com/media/cache/2500x0/catalog/product/1/2/12blue_1622666078.webp",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://mobiles.dailypakistan.com.pk/wp-content/uploads/2022/03/oppoa16e.jpg",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://ae01.alicdn.com/kf/H2372e66c58084287a991b066b386cec1I/For-Huawei-P40-Pro-Case-2020-Release-SUPCASE-UB-Style-Slim-Anti-knock-Premium-Hybrid-Protective.jpg",
                                        Type = MediaType.Photo
                                    }
                                },
                                Specs = new Specs()
                                {
                                    CPU = "Apple A15 Bionic",
                                    Screen = ScreenEnum.AMOLED,
                                    Height = 4.87,
                                    Width = 2.31,
                                    Thickness = 0.3,
                                    CameraMegaPixels = 12,
                                    Color = Colors.Black,
                                    Weight = 144,
                                    OS = OSEnum.IOS,
                                    BatteryCapacity = 2018,
                                    RAM = 6,
                                    Storage = 128
                                }
                            },
                            new Mobile()
                            {
                                Name = "iPhone 13 Pro Max",
                                ReleaseDate = new DateTime(2021, 9, 24),
                                Price = 1099,
                                Description =
                                    "The iPhone 13 Pro Max is Apple's biggest phone in the lineup with a massive, 6.7 screen that for the first time in an iPhone comes with 120Hz ProMotion display that ensures super smooth scrolling.The benefit of such a gigantic phone is that it also comes with the biggest battery of all iPhone 13 series.",
                                WarrantyPeriod = 12 * 2,
                                UnitsInStock = 15,
                                UnitsSold = 800,
                                MainPhotoURL =
                                    "https://www.theiphonewiki.com/w/images/7/7d/IPhone_13_Pro_Max.png",
                                ManufacturerId = 2,
                                Media = new List<Media>()
                                {
                                    new Media()
                                    {
                                        URL =
                                            "https://itstechprice.com/wp-content/uploads/2020/10/vivo-phones.jpg",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://www.theiphonewiki.com/w/images/1/17/IPhone_13_mini.png",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://img01.huaweifile.com/eu/uk/huawei/pms/product/6901443366064/428_428_C2C21356D98ACE0827A3C55CD8AA66DE122A11ED9BCE073Cmp.jpg",
                                        Type = MediaType.Photo
                                    }
                                },
                                Specs = new Specs()
                                {
                                    CPU = "Apple A15 Bionic",
                                    Screen = ScreenEnum.AMOLED,
                                    Height = 4.87,
                                    Width = 2.31,
                                    Thickness = 0.3,
                                    CameraMegaPixels = 12,
                                    Color = Colors.Black,
                                    Weight = 240,
                                    OS = OSEnum.IOS,
                                    BatteryCapacity = 4352,
                                    RAM = 12,
                                    Storage = 128
                                }
                            },
                            new Mobile()
                            {
                                Name = "iPhone 13 Pro",
                                ReleaseDate = new DateTime(2021, 9, 24),
                                Price = 999,
                                Description =
                                    "The iPhone 13 Pro is Apple's smaller premium iPhone with a 6.1 screen size and for the first time in an iPhone, it comes with a 120Hz ProMotion display for super smooth scrolling.The list of innovations includes a more capable triple camera setup, with a wide, ultra - wide and zoom cameras, as well as a LiDAR scanner.",
                                WarrantyPeriod = 12 * 2,
                                UnitsInStock = 10,
                                UnitsSold = 600,
                                MainPhotoURL =
                                    "https://www.theiphonewiki.com/w/images/4/4e/IPhone_13_Pro.png",
                                ManufacturerId = 2,
                                Media = new List<Media>()
                                {
                                    new Media()
                                    {
                                        URL =
                                            "https://www.theiphonewiki.com/w/images/1/17/IPhone_13_mini.png",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://www.theiphonewiki.com/w/images/1/17/IPhone_13_mini.png",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://m.media-amazon.com/images/I/71geVdy6-OS._SX569_.jpg",
                                        Type = MediaType.Photo
                                    }
                                },
                                Specs = new Specs()
                                {
                                    CPU = "Apple A15 Bionic",
                                    Screen = ScreenEnum.AMOLED,
                                    Height = 4.87,
                                    Width = 2.31,
                                    Thickness = 0.3,
                                    CameraMegaPixels = 12,
                                    Color = Colors.Blue,
                                    Weight = 204,
                                    OS = OSEnum.IOS,
                                    BatteryCapacity = 4352,
                                    RAM = 12,
                                    Storage = 512
                                }
                            },
                            new Mobile()
                            {
                                Name = "iPhone 13",
                                ReleaseDate = new DateTime(2021, 9, 24),
                                Price = 999,
                                Description =
                                    "The iPhone 13 is the successor to Apple's best selling iPhone 12, and it improves upon a successful formula: $800 price for a flagship processor and a 6.1-inch screen size that is not too large, nor too small. The iPhone 13 brings a new Apple A15 Bionic chip and improvements to the dual camera setup consisting of a wide and ultra-wide camera.",
                                WarrantyPeriod = 12 * 2,
                                UnitsInStock = 5,
                                UnitsSold = 400,
                                MainPhotoURL =
                                    "https://www.theiphonewiki.com/w/images/3/37/IPhone_13.png",
                                ManufacturerId = 2,
                                Media = new List<Media>()
                                {
                                    new Media()
                                    {
                                        URL =
                                            "https://oxygendigitalshop.com/media/cache/2500x0/catalog/product/1/2/12blue_1622666078.webp",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://www.mobiletech.ie/user/products/mobiletech-p40-silicon-clear1.jpg",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://www.mytrendyphone.ie/images/Original-Silicone-Case-for-Huawei-P40-Pro-51993797-Black-6901443366095-08042020-01-p.jpg",
                                        Type = MediaType.Photo
                                    }
                                },
                                Specs = new Specs()
                                {
                                    CPU = "Apple A15 Bionic",
                                    Screen = ScreenEnum.AMOLED,
                                    Height = 4.87,
                                    Width = 2.31,
                                    Thickness = 0.3,
                                    CameraMegaPixels = 12,
                                    Color = Colors.Blue,
                                    Weight = 174,
                                    OS = OSEnum.IOS,
                                    BatteryCapacity = 3227,
                                    RAM = 16,
                                    Storage = 64
                                }
                            },
                            new Mobile()
                            {
                                Name = "iPhone 13 Mini",
                                ReleaseDate = new DateTime(2021, 9, 24),
                                Price = 999,
                                Description =
                                    "The iPhone 13 Mini is the successor to Apple's first super mini flagship in recent years, the iPhone 12 Mini. It retains the same 5.4 screen size and the one-handed use friendly form factor, while at the same time bringing a faster Apple A15 Bionic processor. On the camera front, the 13 Mini keeps a wide and an ultra - wide setup, and it does not have a dedicated telephoto zoom camera.",
                                WarrantyPeriod = 12 * 2,
                                UnitsInStock = 18,
                                UnitsSold = 800,
                                MainPhotoURL =
                                    "https://www.theiphonewiki.com/w/images/1/17/IPhone_13_mini.png",
                                ManufacturerId = 2,
                                Media = new List<Media>()
                                {
                                    new Media()
                                    {
                                        URL =
                                            "https://www.mobiletech.ie/user/products/mobiletech-p40-silicon-clear1.jpg",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://mobiles.dailypakistan.com.pk/wp-content/uploads/2022/03/oppoa16e.jpg",
                                        Type = MediaType.Photo
                                    },
                                    new Media()
                                    {
                                        URL =
                                            "https://cdn.shopify.com/s/files/1/1126/0898/products/p40-pro-black_650x650.jpg?v=1637573053",
                                        Type = MediaType.Photo
                                    }
                                },
                                Specs = new Specs()
                                {
                                    CPU = "Apple A15 Bionic",
                                    Screen = ScreenEnum.AMOLED,
                                    Height = 4.87,
                                    Width = 2.31,
                                    Thickness = 0.3,
                                    CameraMegaPixels = 12,
                                    Color = Colors.Fuchsia,
                                    Weight = 174,
                                    OS = OSEnum.IOS,
                                    BatteryCapacity = 2406,
                                    RAM = 8,
                                    Storage = 256
                                }
                            }
                        }
                    );
                    context.SaveChanges();
                }

                //Reviews
                //if (!context.Reviews.Any())
                //{
                //    context.Reviews.AddRange(
                //        new List<Review>()
                //        {
                //            //Add Reviews Data
                //            new Review()
                //            {
                //                UserId = "5037b077-356d-41db-8cfe-e2d38ffc75f3",
                //                MobileId = 1,
                //                Rate = MobileRate.good,
                //                Feedback =
                //                    "relatively good for those who search for a practical soultion for mobiles",
                //                Date = DateTime.Now
                //            },
                //            new Review()
                //            {
                //                UserId = "5037b077-356d-41db-8cfe-e2d38ffc75f3",
                //                MobileId = 2,
                //                Rate = MobileRate.moderate,
                //                Feedback = "msh b3raf a2ra 3araby",
                //                Date = DateTime.Now
                //            },
                //            new Review()
                //            {
                //                UserId = "5037b077-356d-41db-8cfe-e2d38ffc75f3",
                //                MobileId = 3,
                //                Rate = MobileRate.exceptional,
                //                Feedback = "gamed fash5",
                //                Date = DateTime.Now
                //            },
                //            new Review()
                //            {
                //                UserId = "5037b077-356d-41db-8cfe-e2d38ffc75f3",
                //                MobileId = 1,
                //                Rate = MobileRate.poor,
                //                Feedback = "zay el zeft",
                //                Date = DateTime.Now
                //            },
                //            new Review()
                //            {
                //                UserId = "5037b077-356d-41db-8cfe-e2d38ffc75f3",
                //                MobileId = 2,
                //                Rate = MobileRate.excellent,
                //                Feedback = "هو فيه ايييييييييييييه",
                //                Date = DateTime.Now
                //            },
                //            new Review()
                //            {
                //                UserId = "5037b077-356d-41db-8cfe-e2d38ffc75f3",
                //                MobileId = 4,
                //                Rate = MobileRate.excellent,
                //                Feedback = "الموبايل ده هيقلب سوهاج كلهاااااااا",
                //                Date = DateTime.Now
                //            },
                            //new Review()
                            //{
                            //    CustomerName = "فتحى",
                            //    MobileId = 3,
                            //    Rate = MobileRate.excellent,
                            //    Feedback = "هى الحياة كدة ليه بقى ليها لون تاااااااااانى",
                            //    Date = DateTime.Now
                            //},
                            //new Review()
                            //{
                            //    CustomerName = "عبده هاشم",
                            //    MobileId = 4,
                            //    Rate = MobileRate.excellent,
                            //    Feedback = "لو لم أكن معايا الموبايل ده لوددت أن يكون معايا",
                            //    Date = DateTime.Now
                            //},
                            //new Review()
                            //{
                            //    CustomerName = "Saleh",
                            //    MobileId = 7,
                            //    Rate = MobileRate.exceptional,
                            //    Feedback = "أقولك إيه واعدلك ايه يا خالد يا بيبو",
                            //    Date = DateTime.Now
                            //},
                            //new Review()
                            //{
                            //    CustomerName = "Wael mawaweel",
                            //    MobileId = 1,
                            //    Rate = MobileRate.good,
                            //    Feedback = "good good good good but not excellent",
                            //    Date = DateTime.Now
                            //},
                            //new Review()
                            //{
                            //    CustomerName = "Samy serag",
                            //    MobileId = 3,
                            //    Rate = MobileRate.exceptional,
                            //    Feedback = "I am speechless",
                            //    Date = DateTime.Now
                            //},
                            //new Review()
                            //{
                            //    CustomerName = "Abo treka",
                            //    MobileId = 2,
                            //    Rate = MobileRate.exceptional,
                            //    Feedback = "Very very good mobile it deserves more than that marketing",
                            //    Date = DateTime.Now
                            //},
                            //new Review()
                            //{
                            //    CustomerName = "Messi",
                            //    MobileId = 6,
                            //    Rate = MobileRate.moderate,
                            //    Feedback = "not bad but I feel like it might be better than that",
                            //    Date = DateTime.Now
                            //}
                        //});
                   // context.SaveChanges();
                }
            }
        }
    }

