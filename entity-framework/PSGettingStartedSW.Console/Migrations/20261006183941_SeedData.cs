using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PSGettingStartedSW.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Length", "Mpaa", "RatingScore", "Title", "Year" },
                values: new object[,]
                {
                    { 1, 182, "R", 9.3000000000000007, "The Shawshank Redemption", 1994 },
                    { 2, 152, "PG-13", 9.0999999999999996, "The Dark Knight", 2008 },
                    { 3, 175, "R", 9.1999999999999993, "The Godfather", 1972 },
                    { 4, 154, "R", 8.8000000000000007, "Pulp", 1994 }
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Age", "FilmId", "FirstName", "Gender", "ImbLink", "LastName" },
                values: new object[,]
                {
                    { 1, 64, 1, "Tim", 1, "https://www.imdb.com/name/nm0000209/", "Robbins" },
                    { 2, 80, 1, "Morgan", 1, "https://www.imdb.com/name/nm0000151/", "Freeman" },
                    { 3, 50, 2, "Christian", 1, "https://www.imdb.com/name/nm0000288/", "Bale" },
                    { 4, 93, 3, "Marlon", 1, "https://www.imdb.com/name/nm0000008/", "Brando" },
                    { 5, 83, 3, "Al", 1, "https://www.imdb.com/name/nm0000199/", "Pacino" },
                    { 6, 73, 4, "John", 1, "https://www.imdb.com/name/nm0000237/", "Travolta" },
                    { 7, 71, 4, "Uma", 0, "https://www.imdb.com/name/nm0000235/", "Thurman" },
                    { 8, 84, 4, "Samuel L.", 1, "https://www.imdb.com/name/nm0000168/", "Jackson" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
