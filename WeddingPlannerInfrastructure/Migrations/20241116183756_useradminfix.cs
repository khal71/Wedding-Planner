using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingPlannerInfrastructure.Migrations
{
    public partial class useradminfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Food",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Flower",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Flower");
        }
    }
}
