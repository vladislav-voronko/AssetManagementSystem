using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BusinessDbContext_UpdateReplenishmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "amount",
                schema: "asset_management",
                table: "replenishments",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "asset_management",
                table: "replenishments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "currency",
                schema: "asset_management",
                table: "replenishments",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "note",
                schema: "asset_management",
                table: "replenishments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "replenished_at",
                schema: "asset_management",
                table: "replenishments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount",
                schema: "asset_management",
                table: "replenishments");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "asset_management",
                table: "replenishments");

            migrationBuilder.DropColumn(
                name: "currency",
                schema: "asset_management",
                table: "replenishments");

            migrationBuilder.DropColumn(
                name: "note",
                schema: "asset_management",
                table: "replenishments");

            migrationBuilder.DropColumn(
                name: "replenished_at",
                schema: "asset_management",
                table: "replenishments");
        }
    }
}
