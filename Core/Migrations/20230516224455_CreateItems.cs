using System;
using System.Numerics;
using Core.DBEntities;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class CreateItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "equipment_id",
                table: "characters",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "toolbar_id",
                table: "characters",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "item_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    item_name = table.Column<string>(type: "text", nullable: false),
                    dimension = table.Column<Vector2>(type: "json", nullable: false),
                    weight = table.Column<float>(type: "real", nullable: false),
                    size = table.Column<int>(type: "integer", nullable: false),
                    equipment = table.Column<int>(type: "integer", nullable: true),
                    toolbar = table.Column<int>(type: "integer", nullable: true),
                    water_use = table.Column<bool>(type: "boolean", nullable: false),
                    inventorydata = table.Column<InventoryItemData>(name: "inventory data", type: "json", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_data", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    item_data_id = table.Column<int>(type: "integer", nullable: false),
                    inventory_id = table.Column<Guid>(type: "uuid", nullable: true),
                    world_position = table.Column<string>(type: "json", nullable: true),
                    content_inventory_id = table.Column<Guid>(type: "uuid", nullable: true),
                    meta = table.Column<ItemMeta>(type: "json", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_items_Inventories_content_inventory_id",
                        column: x => x.content_inventory_id,
                        principalTable: "Inventories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_items_Inventories_inventory_id",
                        column: x => x.inventory_id,
                        principalTable: "Inventories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_items_item_data_item_data_id",
                        column: x => x.item_data_id,
                        principalTable: "item_data",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "equipment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hat_id = table.Column<long>(type: "bigint", nullable: false),
                    head_extra_id = table.Column<long>(type: "bigint", nullable: false),
                    mask_id = table.Column<long>(type: "bigint", nullable: false),
                    neck_id = table.Column<long>(type: "bigint", nullable: false),
                    body_id = table.Column<long>(type: "bigint", nullable: false),
                    legs_id = table.Column<long>(type: "bigint", nullable: false),
                    feet_id = table.Column<long>(type: "bigint", nullable: false),
                    hands_id = table.Column<long>(type: "bigint", nullable: false),
                    watch_id = table.Column<long>(type: "bigint", nullable: false),
                    back_id = table.Column<long>(type: "bigint", nullable: false),
                    armor_west_id = table.Column<long>(type: "bigint", nullable: false),
                    phone_id = table.Column<long>(type: "bigint", nullable: false),
                    tablet_id = table.Column<long>(type: "bigint", nullable: false),
                    radio_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment", x => x.id);
                    table.ForeignKey(
                        name: "FK_equipment_items_armor_west_id",
                        column: x => x.armor_west_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipment_items_back_id",
                        column: x => x.back_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipment_items_body_id",
                        column: x => x.body_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipment_items_feet_id",
                        column: x => x.feet_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipment_items_hands_id",
                        column: x => x.hands_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipment_items_hat_id",
                        column: x => x.hat_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipment_items_head_extra_id",
                        column: x => x.head_extra_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipment_items_legs_id",
                        column: x => x.legs_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipment_items_mask_id",
                        column: x => x.mask_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipment_items_neck_id",
                        column: x => x.neck_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipment_items_phone_id",
                        column: x => x.phone_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipment_items_radio_id",
                        column: x => x.radio_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipment_items_tablet_id",
                        column: x => x.tablet_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipment_items_watch_id",
                        column: x => x.watch_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "toolbars",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    primary_weapon_id = table.Column<long>(type: "bigint", nullable: true),
                    secondary_weapon_id = table.Column<long>(type: "bigint", nullable: true),
                    secondary_extra_weapon_id = table.Column<long>(type: "bigint", nullable: true),
                    melee_id = table.Column<long>(type: "bigint", nullable: true),
                    melee_extra_id = table.Column<long>(type: "bigint", nullable: true),
                    gadget_one_id = table.Column<long>(type: "bigint", nullable: true),
                    gadget_two_id = table.Column<long>(type: "bigint", nullable: true),
                    misc_one_id = table.Column<long>(type: "bigint", nullable: true),
                    misc_two_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_toolbars", x => x.id);
                    table.ForeignKey(
                        name: "FK_toolbars_items_gadget_one_id",
                        column: x => x.gadget_one_id,
                        principalTable: "items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_toolbars_items_gadget_two_id",
                        column: x => x.gadget_two_id,
                        principalTable: "items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_toolbars_items_melee_extra_id",
                        column: x => x.melee_extra_id,
                        principalTable: "items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_toolbars_items_melee_id",
                        column: x => x.melee_id,
                        principalTable: "items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_toolbars_items_misc_one_id",
                        column: x => x.misc_one_id,
                        principalTable: "items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_toolbars_items_misc_two_id",
                        column: x => x.misc_two_id,
                        principalTable: "items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_toolbars_items_primary_weapon_id",
                        column: x => x.primary_weapon_id,
                        principalTable: "items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_toolbars_items_secondary_extra_weapon_id",
                        column: x => x.secondary_extra_weapon_id,
                        principalTable: "items",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_toolbars_items_secondary_weapon_id",
                        column: x => x.secondary_weapon_id,
                        principalTable: "items",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_characters_equipment_id",
                table: "characters",
                column: "equipment_id");

            migrationBuilder.CreateIndex(
                name: "IX_characters_toolbar_id",
                table: "characters",
                column: "toolbar_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_armor_west_id",
                table: "equipment",
                column: "armor_west_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_back_id",
                table: "equipment",
                column: "back_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_body_id",
                table: "equipment",
                column: "body_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_feet_id",
                table: "equipment",
                column: "feet_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_hands_id",
                table: "equipment",
                column: "hands_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_hat_id",
                table: "equipment",
                column: "hat_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_head_extra_id",
                table: "equipment",
                column: "head_extra_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_legs_id",
                table: "equipment",
                column: "legs_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_mask_id",
                table: "equipment",
                column: "mask_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_neck_id",
                table: "equipment",
                column: "neck_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_phone_id",
                table: "equipment",
                column: "phone_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_radio_id",
                table: "equipment",
                column: "radio_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_tablet_id",
                table: "equipment",
                column: "tablet_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipment_watch_id",
                table: "equipment",
                column: "watch_id");

            migrationBuilder.CreateIndex(
                name: "IX_items_content_inventory_id",
                table: "items",
                column: "content_inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_items_inventory_id",
                table: "items",
                column: "inventory_id");

            migrationBuilder.CreateIndex(
                name: "IX_items_item_data_id",
                table: "items",
                column: "item_data_id");

            migrationBuilder.CreateIndex(
                name: "IX_toolbars_gadget_one_id",
                table: "toolbars",
                column: "gadget_one_id");

            migrationBuilder.CreateIndex(
                name: "IX_toolbars_gadget_two_id",
                table: "toolbars",
                column: "gadget_two_id");

            migrationBuilder.CreateIndex(
                name: "IX_toolbars_melee_extra_id",
                table: "toolbars",
                column: "melee_extra_id");

            migrationBuilder.CreateIndex(
                name: "IX_toolbars_melee_id",
                table: "toolbars",
                column: "melee_id");

            migrationBuilder.CreateIndex(
                name: "IX_toolbars_misc_one_id",
                table: "toolbars",
                column: "misc_one_id");

            migrationBuilder.CreateIndex(
                name: "IX_toolbars_misc_two_id",
                table: "toolbars",
                column: "misc_two_id");

            migrationBuilder.CreateIndex(
                name: "IX_toolbars_primary_weapon_id",
                table: "toolbars",
                column: "primary_weapon_id");

            migrationBuilder.CreateIndex(
                name: "IX_toolbars_secondary_extra_weapon_id",
                table: "toolbars",
                column: "secondary_extra_weapon_id");

            migrationBuilder.CreateIndex(
                name: "IX_toolbars_secondary_weapon_id",
                table: "toolbars",
                column: "secondary_weapon_id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_characters_equipment_equipment_id",
                table: "characters");

            migrationBuilder.DropForeignKey(
                name: "FK_characters_toolbars_toolbar_id",
                table: "characters");

            migrationBuilder.DropTable(
                name: "equipment");

            migrationBuilder.DropTable(
                name: "toolbars");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "item_data");

            migrationBuilder.DropIndex(
                name: "IX_characters_equipment_id",
                table: "characters");

            migrationBuilder.DropIndex(
                name: "IX_characters_toolbar_id",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "equipment_id",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "toolbar_id",
                table: "characters");
        }
    }
}
