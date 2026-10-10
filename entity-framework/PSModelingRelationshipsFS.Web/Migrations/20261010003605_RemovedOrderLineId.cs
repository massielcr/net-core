using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSModelingRelationshipsFS.Web.Migrations
{
    /// <inheritdoc />
    public partial class RemovedOrderLineId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderLines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderLines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
