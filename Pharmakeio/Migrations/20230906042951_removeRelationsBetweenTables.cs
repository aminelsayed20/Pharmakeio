using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmakeio.Migrations
{
    /// <inheritdoc />
    public partial class removeRelationsBetweenTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_PharmaceuticalDepartments_DepartmentId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_DepartmentId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Medicines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_DepartmentId",
                table: "Medicines",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_PharmaceuticalDepartments_DepartmentId",
                table: "Medicines",
                column: "DepartmentId",
                principalTable: "PharmaceuticalDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
