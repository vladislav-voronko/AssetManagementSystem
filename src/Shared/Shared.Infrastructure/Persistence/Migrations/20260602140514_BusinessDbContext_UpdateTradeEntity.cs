using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BusinessDbContext_UpdateTradeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "asset_id",
                schema: "asset_management",
                table: "trades",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "asset_management",
                table: "trades",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "executed_at",
                schema: "asset_management",
                table: "trades",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                schema: "asset_management",
                table: "trades",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "quantity",
                schema: "asset_management",
                table: "trades",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "type",
                schema: "asset_management",
                table: "trades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_trades_asset_id",
                schema: "asset_management",
                table: "trades",
                column: "asset_id");

            migrationBuilder.AddForeignKey(
                name: "fk_trades_assets_asset_id",
                schema: "asset_management",
                table: "trades",
                column: "asset_id",
                principalSchema: "asset_management",
                principalTable: "assets",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_trades_assets_asset_id",
                schema: "asset_management",
                table: "trades");

            migrationBuilder.DropIndex(
                name: "ix_trades_asset_id",
                schema: "asset_management",
                table: "trades");

            migrationBuilder.DropColumn(
                name: "asset_id",
                schema: "asset_management",
                table: "trades");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "asset_management",
                table: "trades");

            migrationBuilder.DropColumn(
                name: "executed_at",
                schema: "asset_management",
                table: "trades");

            migrationBuilder.DropColumn(
                name: "price",
                schema: "asset_management",
                table: "trades");

            migrationBuilder.DropColumn(
                name: "quantity",
                schema: "asset_management",
                table: "trades");

            migrationBuilder.DropColumn(
                name: "type",
                schema: "asset_management",
                table: "trades");
        }
    }
}
