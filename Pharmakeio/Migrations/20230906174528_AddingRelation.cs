using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmakeio.Migrations
{
    /// <inheritdoc />
    public partial class AddingRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PharmaceuticalDepartmentId",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_PharmaceuticalDepartmentId",
                table: "Medicines",
                column: "PharmaceuticalDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_PharmaceuticalDepartments_PharmaceuticalDepartmentId",
                table: "Medicines",
                column: "PharmaceuticalDepartmentId",
                principalTable: "PharmaceuticalDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_PharmaceuticalDepartments_PharmaceuticalDepartmentId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_PharmaceuticalDepartmentId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "PharmaceuticalDepartmentId",
                table: "Medicines");
        }
    }
}
