using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSModelingRelationshipsFS.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "SalesAgentId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerProfile",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LoyaltyTier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirthUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProfile", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_CustomerProfile_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesAgent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesAgent", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SalesAgentId",
                table: "Orders",
                column: "SalesAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SalesAgent_SalesAgentId",
                table: "Orders",
                column: "SalesAgentId",
                principalTable: "SalesAgent",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SalesAgent_SalesAgentId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "CustomerProfile");

            migrationBuilder.DropTable(
                name: "SalesAgent");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SalesAgentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SalesAgentId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
