using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Shared.Models;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class Inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    acp_id = table.Column<string>(type: "text", nullable: false),
                    ips = table.Column<string[]>(type: "text[]", nullable: true),
                    hardware = table.Column<decimal[]>(type: "numeric(20,0)[]", nullable: true),
                    last_login = table.Column<ZonedDateTime>(type: "timestamptz", nullable: false),
                    banned = table.Column<bool>(type: "boolean", nullable: false),
                    bann_reason = table.Column<string>(type: "text", nullable: true),
                    character_limit = table.Column<short>(type: "smallint", nullable: false),
                    ped_limit = table.Column<short>(type: "smallint", nullable: false),
                    animal_limit = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    model = table.Column<string>(type: "text", nullable: false),
                    @class = table.Column<string>(name: "class", type: "text", nullable: false),
                    base_max_fuel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_data", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "characters",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    perso_id = table.Column<int>(type: "integer", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dimension = table.Column<int>(type: "integer", nullable: false),
                    position = table.Column<string>(type: "json", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    health = table.Column<int>(type: "integer", nullable: true),
                    is_dead = table.Column<bool>(type: "boolean", nullable: false),
                    hours = table.Column<float>(type: "real", nullable: false),
                    info = table.Column<CharacterInfo>(type: "json", nullable: false),
                    appearance = table.Column<Appearance>(type: "json", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characters", x => x.id);
                    table.ForeignKey(
                        name: "FK_characters_accounts_account_id",
                        column: x => x.account_id,
                        principalTable: "accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    plate = table.Column<string>(type: "text", nullable: false),
                    vehicle_data_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModelId = table.Column<int>(type: "integer", nullable: false),
                    position = table.Column<string>(type: "json", nullable: false),
                    rotation = table.Column<string>(type: "json", nullable: false),
                    fuel = table.Column<int>(type: "integer", nullable: false),
                    interior = table.Column<string>(type: "text", nullable: false),
                    lastUsed = table.Column<ZonedDateTime>(type: "timestamptz", nullable: false),
                    damage = table.Column<Guid>(type: "json", nullable: false),
                    tuning = table.Column<Guid>(type: "json", nullable: false),
                    dirt = table.Column<int>(type: "integer", nullable: false),
                    behavior = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<ZonedDateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.id);
                    table.ForeignKey(
                        name: "FK_vehicles_vehicle_data_ModelId",
                        column: x => x.ModelId,
                        principalTable: "vehicle_data",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_keys",
                columns: table => new
                {
                    character_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_keys", x => new { x.character_id, x.vehicle_id });
                    table.ForeignKey(
                        name: "FK_vehicle_keys_characters_character_id",
                        column: x => x.character_id,
                        principalTable: "characters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vehicle_keys_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_registrations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehicle_id = table.Column<Guid>(type: "uuid", nullable: false),
                    plate = table.Column<string>(type: "text", nullable: false),
                    registered_at = table.Column<ZonedDateTime>(type: "timestamptz", nullable: false),
                    registered_by_id = table.Column<Guid>(type: "uuid", nullable: false),
                    unregistered_at = table.Column<ZonedDateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_registrations", x => x.id);
                    table.ForeignKey(
                        name: "FK_vehicle_registrations_characters_registered_by_id",
                        column: x => x.registered_by_id,
                        principalTable: "characters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vehicle_registrations_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_characters_account_id",
                table: "characters",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_keys_vehicle_id",
                table: "vehicle_keys",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_registrations_registered_by_id",
                table: "vehicle_registrations",
                column: "registered_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_registrations_vehicle_id",
                table: "vehicle_registrations",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_ModelId",
                table: "vehicles",
                column: "ModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vehicle_keys");

            migrationBuilder.DropTable(
                name: "vehicle_registrations");

            migrationBuilder.DropTable(
                name: "characters");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "vehicle_data");
        }
    }
}
