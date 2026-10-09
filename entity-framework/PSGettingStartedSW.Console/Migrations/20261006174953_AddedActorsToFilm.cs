using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSGettingStartedSW.Migrations
{
    /// <inheritdoc />
    public partial class AddedActorsToFilm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Films",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "FilmId",
                table: "Actors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actors_FilmId",
                table: "Actors",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Films_FilmId",
                table: "Actors",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Films_FilmId",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_FilmId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "FilmId",
                table: "Actors");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
