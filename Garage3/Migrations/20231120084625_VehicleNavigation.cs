using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage3.Migrations
{
    /// <inheritdoc />
    public partial class VehicleNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_ParkingSpaces_ParkingSpaceId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_ParkingSpaceId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "ParkingSpaceId",
                table: "Vehicles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ParkingSpaceId",
                table: "Vehicles",
                column: "ParkingSpaceId",
                unique: true,
                filter: "[ParkingSpaceId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_ParkingSpaces_ParkingSpaceId",
                table: "Vehicles",
                column: "ParkingSpaceId",
                principalTable: "ParkingSpaces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_ParkingSpaces_ParkingSpaceId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_ParkingSpaceId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<int>(
                name: "ParkingSpaceId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ParkingSpaceId",
                table: "Vehicles",
                column: "ParkingSpaceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_ParkingSpaces_ParkingSpaceId",
                table: "Vehicles",
                column: "ParkingSpaceId",
                principalTable: "ParkingSpaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
