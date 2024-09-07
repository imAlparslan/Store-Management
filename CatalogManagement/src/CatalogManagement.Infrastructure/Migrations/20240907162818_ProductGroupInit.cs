using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductGroupInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductGroup",
                schema: "Catalog",
                columns: table => new
                {
                    ProductGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.ProductGroupId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductGroup",
                schema: "Catalog");
        }
    }
}
