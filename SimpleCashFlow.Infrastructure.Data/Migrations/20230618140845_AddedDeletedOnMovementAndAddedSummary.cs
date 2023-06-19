using Microsoft.EntityFrameworkCore.Migrations;
using SimpleCashFlow.Domain.ValueObjects;

#nullable disable

namespace SimpleCashFlow.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDeletedOnMovementAndAddedSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ChangedAt",
                table: "Movements",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Movements",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Movements",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DomainEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MovementId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomainEvent_Movements_MovementId",
                        column: x => x.MovementId,
                        principalTable: "Movements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Summaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalCreditAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalDebitAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    MovementItems = table.Column<List<CashFlowDailySummary.MovementItem>>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summaries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomainEvent_MovementId",
                table: "DomainEvent",
                column: "MovementId");

            migrationBuilder.CreateIndex(
                name: "IX_Summaries_Date",
                table: "Summaries",
                column: "Date",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainEvent");

            migrationBuilder.DropTable(
                name: "Summaries");

            migrationBuilder.DropColumn(
                name: "ChangedAt",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Movements");
        }
    }
}
