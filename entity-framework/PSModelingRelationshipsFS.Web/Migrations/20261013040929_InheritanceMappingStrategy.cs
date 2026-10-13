using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSModelingRelationshipsFS.Web.Migrations
{
    /// <inheritdoc />
    public partial class InheritanceMappingStrategy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProfile_Customers_CustomerId",
                table: "CustomerProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SalesAgent_SalesAgentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSupliers_Suplier_SuplierId",
                table: "ProductSupliers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Tag_TagId",
                table: "ProductTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suplier",
                table: "Suplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesAgent",
                table: "SalesAgent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerProfile",
                table: "CustomerProfile");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Suplier",
                newName: "Supliers");

            migrationBuilder.RenameTable(
                name: "SalesAgent",
                newName: "SalesAgents");

            migrationBuilder.RenameTable(
                name: "CustomerProfile",
                newName: "CustomerProfiles");

            migrationBuilder.AddColumn<bool>(
                name: "AutoRenews",
                table: "Products",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillingPeriodMonths",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DimHeightCm",
                table: "Products",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DimLenghtCm",
                table: "Products",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DimWidthCm",
                table: "Products",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DownloadUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FileSizeBytes",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductKind",
                table: "Products",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "RequiresSignature",
                table: "Products",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "TrialEndsOn",
                table: "Products",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WeigthKg",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supliers",
                table: "Supliers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesAgents",
                table: "SalesAgents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerProfiles",
                table: "CustomerProfiles",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProfiles_Customers_CustomerId",
                table: "CustomerProfiles",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SalesAgents_SalesAgentId",
                table: "Orders",
                column: "SalesAgentId",
                principalTable: "SalesAgents",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupliers_Supliers_SuplierId",
                table: "ProductSupliers",
                column: "SuplierId",
                principalTable: "Supliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Tags_TagId",
                table: "ProductTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProfiles_Customers_CustomerId",
                table: "CustomerProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SalesAgents_SalesAgentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSupliers_Supliers_SuplierId",
                table: "ProductSupliers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Tags_TagId",
                table: "ProductTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supliers",
                table: "Supliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesAgents",
                table: "SalesAgents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerProfiles",
                table: "CustomerProfiles");

            migrationBuilder.DropColumn(
                name: "AutoRenews",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BillingPeriodMonths",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DimHeightCm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DimLenghtCm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DimWidthCm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DownloadUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FileSizeBytes",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductKind",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RequiresSignature",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TrialEndsOn",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WeigthKg",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "Supliers",
                newName: "Suplier");

            migrationBuilder.RenameTable(
                name: "SalesAgents",
                newName: "SalesAgent");

            migrationBuilder.RenameTable(
                name: "CustomerProfiles",
                newName: "CustomerProfile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suplier",
                table: "Suplier",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesAgent",
                table: "SalesAgent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerProfile",
                table: "CustomerProfile",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProfile_Customers_CustomerId",
                table: "CustomerProfile",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SalesAgent_SalesAgentId",
                table: "Orders",
                column: "SalesAgentId",
                principalTable: "SalesAgent",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupliers_Suplier_SuplierId",
                table: "ProductSupliers",
                column: "SuplierId",
                principalTable: "Suplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Tag_TagId",
                table: "ProductTags",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
