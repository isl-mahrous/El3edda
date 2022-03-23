using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace El3edda.Migrations
{
    public partial class addedImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobile_Manufacturers_ManufacturerId",
                table: "Mobile");

            migrationBuilder.RenameColumn(
                name: "ManufacturerId",
                table: "Mobile",
                newName: "ManID");

            migrationBuilder.RenameIndex(
                name: "IX_Mobile_ManufacturerId",
                table: "Mobile",
                newName: "IX_Mobile_ManID");

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Mobile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Mobile_Manufacturers_ManID",
                table: "Mobile",
                column: "ManID",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobile_Manufacturers_ManID",
                table: "Mobile");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Mobile");

            migrationBuilder.RenameColumn(
                name: "ManID",
                table: "Mobile",
                newName: "ManufacturerId");

            migrationBuilder.RenameIndex(
                name: "IX_Mobile_ManID",
                table: "Mobile",
                newName: "IX_Mobile_ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mobile_Manufacturers_ManufacturerId",
                table: "Mobile",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
