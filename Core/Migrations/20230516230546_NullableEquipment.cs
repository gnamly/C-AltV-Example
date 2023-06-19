using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class NullableEquipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_armor_west_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_back_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_body_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_feet_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_hands_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_hat_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_head_extra_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_legs_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_mask_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_neck_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_phone_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_radio_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_tablet_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_watch_id",
                table: "equipment");

            migrationBuilder.AlterColumn<long>(
                name: "watch_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "tablet_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "radio_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "phone_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "neck_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "mask_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "legs_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "head_extra_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "hat_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "hands_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "feet_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "body_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "back_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "armor_west_id",
                table: "equipment",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_armor_west_id",
                table: "equipment",
                column: "armor_west_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_back_id",
                table: "equipment",
                column: "back_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_body_id",
                table: "equipment",
                column: "body_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_feet_id",
                table: "equipment",
                column: "feet_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_hands_id",
                table: "equipment",
                column: "hands_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_hat_id",
                table: "equipment",
                column: "hat_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_head_extra_id",
                table: "equipment",
                column: "head_extra_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_legs_id",
                table: "equipment",
                column: "legs_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_mask_id",
                table: "equipment",
                column: "mask_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_neck_id",
                table: "equipment",
                column: "neck_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_phone_id",
                table: "equipment",
                column: "phone_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_radio_id",
                table: "equipment",
                column: "radio_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_tablet_id",
                table: "equipment",
                column: "tablet_id",
                principalTable: "items",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_watch_id",
                table: "equipment",
                column: "watch_id",
                principalTable: "items",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_armor_west_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_back_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_body_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_feet_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_hands_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_hat_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_head_extra_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_legs_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_mask_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_neck_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_phone_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_radio_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_tablet_id",
                table: "equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_equipment_items_watch_id",
                table: "equipment");

            migrationBuilder.AlterColumn<long>(
                name: "watch_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "tablet_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "radio_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "phone_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "neck_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "mask_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "legs_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "head_extra_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "hat_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "hands_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "feet_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "body_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "back_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "armor_west_id",
                table: "equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_armor_west_id",
                table: "equipment",
                column: "armor_west_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_back_id",
                table: "equipment",
                column: "back_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_body_id",
                table: "equipment",
                column: "body_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_feet_id",
                table: "equipment",
                column: "feet_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_hands_id",
                table: "equipment",
                column: "hands_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_hat_id",
                table: "equipment",
                column: "hat_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_head_extra_id",
                table: "equipment",
                column: "head_extra_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_legs_id",
                table: "equipment",
                column: "legs_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_mask_id",
                table: "equipment",
                column: "mask_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_neck_id",
                table: "equipment",
                column: "neck_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_phone_id",
                table: "equipment",
                column: "phone_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_radio_id",
                table: "equipment",
                column: "radio_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_tablet_id",
                table: "equipment",
                column: "tablet_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipment_items_watch_id",
                table: "equipment",
                column: "watch_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
