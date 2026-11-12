using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSDDDCQRSInPracticeLB.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAggregates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_JobRoles_JobRoleId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalaries_EmployeeId",
                table: "EmployeeSalaries");

            migrationBuilder.DropIndex(
                name: "IX_Employees_JobRoleId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "To",
                table: "StoreAssignments");

            migrationBuilder.DropColumn(
                name: "To",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "JobRoleId",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "EmployeeSalaries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<DateOnly>(type: "date", nullable: false),
                    To = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreAssignmentHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    JobRoleId = table.Column<int>(type: "int", nullable: false),
                    From = table.Column<DateOnly>(type: "date", nullable: false),
                    To = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreAssignmentHistories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_EmployeeId",
                table: "EmployeeSalaries",
                column: "EmployeeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeSalaryHistories");

            migrationBuilder.DropTable(
                name: "StoreAssignmentHistories");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalaries_EmployeeId",
                table: "EmployeeSalaries");

            migrationBuilder.AddColumn<DateOnly>(
                name: "To",
                table: "StoreAssignments",
                type: "date",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "EmployeeSalaries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "To",
                table: "EmployeeSalaries",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobRoleId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_EmployeeId",
                table: "EmployeeSalaries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobRoleId",
                table: "Employees",
                column: "JobRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_JobRoles_JobRoleId",
                table: "Employees",
                column: "JobRoleId",
                principalTable: "JobRoles",
                principalColumn: "Id");
        }
    }
}
