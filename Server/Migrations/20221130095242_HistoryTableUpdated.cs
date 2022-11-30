using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceDashboard.Server.Migrations
{
    /// <inheritdoc />
    public partial class HistoryTableUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Company_CompanyId",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_CompanyId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "History");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "History",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanySymbol",
                table: "History",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "History");

            migrationBuilder.DropColumn(
                name: "CompanySymbol",
                table: "History");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "History",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_History_CompanyId",
                table: "History",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Company_CompanyId",
                table: "History",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
