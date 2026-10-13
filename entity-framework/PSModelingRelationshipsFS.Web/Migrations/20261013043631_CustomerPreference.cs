using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSModelingRelationshipsFS.Web.Migrations
{
    /// <inheritdoc />
    public partial class CustomerPreference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Preferences",
                table: "Customers",
                type: "json",
                nullable: false,
                defaultValue: "{}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preferences",
                table: "Customers");
        }
    }
}
