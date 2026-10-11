using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSModelingRelationshipsFS.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddedAddressAndMoney : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PriceCurrency",
                table: "Products",
                type: "char(3)",
                unicode: false,
                fixedLength: true,
                nullable: false,
                defaultValue: "USD");

            migrationBuilder.AddColumn<string>(
                name: "ShipToCity",
                table: "Orders",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipToCountryCode",
                table: "Orders",
                type: "char(2)",
                unicode: false,
                fixedLength: true,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipToPostalCode",
                table: "Orders",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipToStreet",
                table: "Orders",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnitPriceCurrency",
                table: "OrderLines",
                type: "char(3)",
                unicode: false,
                fixedLength: true,
                nullable: false,
                defaultValue: "USD");

            migrationBuilder.AddColumn<string>(
                name: "BillCity",
                table: "Customers",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillCountryCode",
                table: "Customers",
                type: "char(2)",
                unicode: false,
                fixedLength: true,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillPostalCode",
                table: "Customers",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillStreet",
                table: "Customers",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BillingAddress_BillingPresent",
                table: "Customers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipCity",
                table: "Customers",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipCountryCode",
                table: "Customers",
                type: "char(2)",
                unicode: false,
                fixedLength: true,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipPostalCode",
                table: "Customers",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipStreet",
                table: "Customers",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceCurrency",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShipToCity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipToCountryCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipToPostalCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShipToStreet",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UnitPriceCurrency",
                table: "OrderLines");

            migrationBuilder.DropColumn(
                name: "BillCity",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillCountryCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillPostalCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillStreet",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BillingAddress_BillingPresent",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShipCity",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShipCountryCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShipPostalCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShipStreet",
                table: "Customers");
        }
    }
}
