using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace El3edda.Migrations
{
    public partial class BillingAddressRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
