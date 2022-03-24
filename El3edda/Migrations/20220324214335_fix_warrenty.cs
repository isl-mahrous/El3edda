using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace El3edda.Migrations
{
    public partial class fix_warrenty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WarrantyPeriod",
                table: "Mobiles",
                type: "int",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "WarrantyPeriod",
                table: "Mobiles",
                type: "time",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
