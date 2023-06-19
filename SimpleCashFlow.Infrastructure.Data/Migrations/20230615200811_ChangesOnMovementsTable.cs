using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleCashFlow.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesOnMovementsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Movemnts",
                table: "Movemnts");

            migrationBuilder.RenameTable(
                name: "Movemnts",
                newName: "Movements");

            migrationBuilder.RenameIndex(
                name: "IX_Movemnts_Date",
                table: "Movements",
                newName: "IX_Movements_Date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movements",
                table: "Movements",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Movements",
                table: "Movements");

            migrationBuilder.RenameTable(
                name: "Movements",
                newName: "Movemnts");

            migrationBuilder.RenameIndex(
                name: "IX_Movements_Date",
                table: "Movemnts",
                newName: "IX_Movemnts_Date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movemnts",
                table: "Movemnts",
                column: "Id");
        }
    }
}
