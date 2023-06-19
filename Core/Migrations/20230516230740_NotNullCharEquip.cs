using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class NotNullCharEquip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characters_equipment_equipment_id",
                table: "characters");

            migrationBuilder.DropForeignKey(
                name: "FK_characters_toolbars_toolbar_id",
                table: "characters");

            migrationBuilder.AlterColumn<Guid>(
                name: "toolbar_id",
                table: "characters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "equipment_id",
                table: "characters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_characters_equipment_equipment_id",
                table: "characters",
                column: "equipment_id",
                principalTable: "equipment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_characters_toolbars_toolbar_id",
                table: "characters",
                column: "toolbar_id",
                principalTable: "toolbars",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characters_equipment_equipment_id",
                table: "characters");

            migrationBuilder.DropForeignKey(
                name: "FK_characters_toolbars_toolbar_id",
                table: "characters");

            migrationBuilder.AlterColumn<Guid>(
                name: "toolbar_id",
                table: "characters",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "equipment_id",
                table: "characters",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_characters_equipment_equipment_id",
                table: "characters",
                column: "equipment_id",
                principalTable: "equipment",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_characters_toolbars_toolbar_id",
                table: "characters",
                column: "toolbar_id",
                principalTable: "toolbars",
                principalColumn: "id");
        }
    }
}
