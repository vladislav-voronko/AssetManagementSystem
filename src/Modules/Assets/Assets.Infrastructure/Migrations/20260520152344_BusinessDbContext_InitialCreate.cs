using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BusinessDbContext_InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "asset_management");

            migrationBuilder.CreateTable(
                name: "assets",
                schema: "asset_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "replenishments",
                schema: "asset_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_replenishments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trades",
                schema: "asset_management",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trades", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assets",
                schema: "asset_management");

            migrationBuilder.DropTable(
                name: "replenishments",
                schema: "asset_management");

            migrationBuilder.DropTable(
                name: "trades",
                schema: "asset_management");
        }
    }
}
