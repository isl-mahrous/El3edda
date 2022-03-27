using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace El3edda.Migrations
{
    public partial class AddressModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Neighbourhood",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_State",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Street",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_Neighbourhood",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_State",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_Street",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddress_City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Neighbourhood",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BillingAddress_State",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Street",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_Neighbourhood",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_State",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_Street",
                table: "AspNetUsers");
        }
    }
}
