using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmakeio.Migrations
{
    /// <inheritdoc />
    public partial class addingNumOfProductsToDepartTabdle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "PharmaceuticalDepartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PharmaceuticalDepartments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfProducts",
                table: "PharmaceuticalDepartments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "PharmaceuticalDepartments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PharmaceuticalDepartments");

            migrationBuilder.DropColumn(
                name: "NumberOfProducts",
                table: "PharmaceuticalDepartments");
        }
    }
}
