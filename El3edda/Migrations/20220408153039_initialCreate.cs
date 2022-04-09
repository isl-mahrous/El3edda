using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace El3edda.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "AspNetRoles",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUsers",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ShippingAddress_Neighbourhood = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ShippingAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ShippingAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ShippingAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Manufacturers",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        Origin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Manufacturers", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetRoleClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "AspNetRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserClaims",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserLogins",
            //    columns: table => new
            //    {
            //        LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserRoles",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "AspNetRoles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "AspNetUserTokens",
            //    columns: table => new
            //    {
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //        table.ForeignKey(
            //            name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Orders",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        ShippingAddress_Neighbourhood = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ShippingAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ShippingAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ShippingAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        orderState = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Orders", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Orders_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Mobiles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Price = table.Column<double>(type: "float", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
            //        WarrantyPeriod = table.Column<int>(type: "int", nullable: false),
            //        UnitsInStock = table.Column<int>(type: "int", nullable: false),
            //        MainPhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        UnitsSold = table.Column<int>(type: "int", nullable: false),
            //        Specs_CPU = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Specs_Screen = table.Column<int>(type: "int", nullable: false),
            //        Specs_Height = table.Column<double>(type: "float", nullable: false),
            //        Specs_Width = table.Column<double>(type: "float", nullable: false),
            //        Specs_Thickness = table.Column<double>(type: "float", nullable: false),
            //        Specs_CameraMegaPixels = table.Column<double>(type: "float", nullable: false),
            //        Specs_Color = table.Column<int>(type: "int", nullable: false),
            //        Specs_Weight = table.Column<double>(type: "float", nullable: false),
            //        Specs_OS = table.Column<byte>(type: "tinyint", nullable: false),
            //        Specs_BatteryCapacity = table.Column<int>(type: "int", nullable: false),
            //        Specs_RAM = table.Column<int>(type: "int", nullable: false),
            //        Specs_Storage = table.Column<int>(type: "int", nullable: false),
            //        ManufacturerId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Mobiles", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Mobiles_Manufacturers_ManufacturerId",
            //            column: x => x.ManufacturerId,
            //            principalTable: "Manufacturers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Media",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Type = table.Column<int>(type: "int", nullable: false),
            //        URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        MobileId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Media", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Media_Mobiles_MobileId",
            //            column: x => x.MobileId,
            //            principalTable: "Mobiles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrderItems",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Amount = table.Column<int>(type: "int", nullable: false),
            //        Price = table.Column<double>(type: "float", nullable: false),
            //        MobileId = table.Column<int>(type: "int", nullable: false),
            //        OrderId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrderItems", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_OrderItems_Mobiles_MobileId",
            //            column: x => x.MobileId,
            //            principalTable: "Mobiles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_OrderItems_Orders_OrderId",
            //            column: x => x.OrderId,
            //            principalTable: "Orders",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Reviews",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Rate = table.Column<int>(type: "int", nullable: false),
            //        Feedback = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        Date = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        MobileId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Reviews", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Reviews_Mobiles_MobileId",
            //            column: x => x.MobileId,
            //            principalTable: "Mobiles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ShoppingCartItems",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Amount = table.Column<int>(type: "int", nullable: false),
            //        ShoppingCartId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MobileId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ShoppingCartItems_Mobiles_MobileId",
            //            column: x => x.MobileId,
            //            principalTable: "Mobiles",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetRoleClaims_RoleId",
            //    table: "AspNetRoleClaims",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "RoleNameIndex",
            //    table: "AspNetRoles",
            //    column: "NormalizedName",
            //    unique: true,
            //    filter: "[NormalizedName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserClaims_UserId",
            //    table: "AspNetUserClaims",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserLogins_UserId",
            //    table: "AspNetUserLogins",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserRoles_RoleId",
            //    table: "AspNetUserRoles",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "EmailIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedEmail");

            //migrationBuilder.CreateIndex(
            //    name: "UserNameIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedUserName",
            //    unique: true,
            //    filter: "[NormalizedUserName] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Media_MobileId",
            //    table: "Media",
            //    column: "MobileId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Mobiles_ManufacturerId",
            //    table: "Mobiles",
            //    column: "ManufacturerId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderItems_MobileId",
            //    table: "OrderItems",
            //    column: "MobileId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderItems_OrderId",
            //    table: "OrderItems",
            //    column: "OrderId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Orders_UserId",
            //    table: "Orders",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Reviews_MobileId",
            //    table: "Reviews",
            //    column: "MobileId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ShoppingCartItems_MobileId",
            //    table: "ShoppingCartItems",
            //    column: "MobileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AspNetRoleClaims");

            migrationBuilder.DropTable(name: "AspNetUserClaims");

            migrationBuilder.DropTable(name: "AspNetUserLogins");

            migrationBuilder.DropTable(name: "AspNetUserRoles");

            migrationBuilder.DropTable(name: "AspNetUserTokens");

            migrationBuilder.DropTable(name: "Media");

            migrationBuilder.DropTable(name: "OrderItems");

            migrationBuilder.DropTable(name: "Reviews");

            migrationBuilder.DropTable(name: "ShoppingCartItems");

            migrationBuilder.DropTable(name: "AspNetRoles");

            migrationBuilder.DropTable(name: "Orders");

            migrationBuilder.DropTable(name: "Mobiles");

            migrationBuilder.DropTable(name: "AspNetUsers");

            migrationBuilder.DropTable(name: "Manufacturers");
        }
    }
}
