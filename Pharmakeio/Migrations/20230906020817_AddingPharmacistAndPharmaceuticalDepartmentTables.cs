using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmakeio.Migrations
{
    /// <inheritdoc />
    public partial class AddingPharmacistAndPharmaceuticalDepartmentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_PharmaceuticalDepartment_DepartmentId",
                table: "Medicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PharmaceuticalDepartment",
                table: "PharmaceuticalDepartment");

            migrationBuilder.RenameTable(
                name: "PharmaceuticalDepartment",
                newName: "PharmaceuticalDepartments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PharmaceuticalDepartments",
                table: "PharmaceuticalDepartments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Pharmacists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacists", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_PharmaceuticalDepartments_DepartmentId",
                table: "Medicines",
                column: "DepartmentId",
                principalTable: "PharmaceuticalDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_PharmaceuticalDepartments_DepartmentId",
                table: "Medicines");

            migrationBuilder.DropTable(
                name: "Pharmacists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PharmaceuticalDepartments",
                table: "PharmaceuticalDepartments");

            migrationBuilder.RenameTable(
                name: "PharmaceuticalDepartments",
                newName: "PharmaceuticalDepartment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PharmaceuticalDepartment",
                table: "PharmaceuticalDepartment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_PharmaceuticalDepartment_DepartmentId",
                table: "Medicines",
                column: "DepartmentId",
                principalTable: "PharmaceuticalDepartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
