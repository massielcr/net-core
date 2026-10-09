using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSGettingStartedSW.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataFixTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 4,
                column: "Title",
                value: "Pulp Fiction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 4,
                column: "Title",
                value: "Pulp");
        }
    }
}
