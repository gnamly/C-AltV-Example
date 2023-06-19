using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class FixVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicles_vehicle_data_ModelId",
                table: "vehicles");

            migrationBuilder.DropIndex(
                name: "IX_vehicles_ModelId",
                table: "vehicles");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "vehicles");

            migrationBuilder.DropColumn(
                name: "behavior",
                table: "vehicles");

            //Can not be changed automaticly
            // migrationBuilder.AlterColumn<int>(
            //     name: "vehicle_data_id",
            //     table: "vehicles",
            //     type: "integer",
            //     nullable: false,
            //     oldClrType: typeof(Guid),
            //     oldType: "uuid");
            
            //Fix for alter column
            migrationBuilder.DropColumn(
                name: "vehicle_data_id",
                table: "vehicles");

            migrationBuilder.AddColumn<int>(
                name: "vehicle_data_id",
                table: "vehicles",
                type: "integer",
                nullable: false
            );

            migrationBuilder.AlterColumn<string>(
                name: "interior",
                table: "vehicles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<byte>(
                name: "dirt",
                table: "vehicles",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_vehicle_data_id",
                table: "vehicles",
                column: "vehicle_data_id");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicles_vehicle_data_vehicle_data_id",
                table: "vehicles",
                column: "vehicle_data_id",
                principalTable: "vehicle_data",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vehicles_vehicle_data_vehicle_data_id",
                table: "vehicles");

            migrationBuilder.DropIndex(
                name: "IX_vehicles_vehicle_data_id",
                table: "vehicles");
            
            // migrationBuilder.AlterColumn<Guid>(
            //     name: "vehicle_data_id",
            //     table: "vehicles",
            //     type: "uuid",
            //     nullable: false,
            //     oldClrType: typeof(int),
            //     oldType: "integer");
            
            //Fix for alter column
            migrationBuilder.DropColumn(
                name: "vehicle_data_id",
                table: "vehicles");

            migrationBuilder.AddColumn<Guid>(
                name: "vehicle_data_id",
                table: "vehicles",
                type: "uuid",
                nullable: false
            );

            migrationBuilder.AlterColumn<string>(
                name: "interior",
                table: "vehicles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "dirt",
                table: "vehicles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "vehicles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "behavior",
                table: "vehicles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_ModelId",
                table: "vehicles",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_vehicles_vehicle_data_ModelId",
                table: "vehicles",
                column: "ModelId",
                principalTable: "vehicle_data",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
