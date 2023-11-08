using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage2.Migrations
{
    /// <inheritdoc />
    public partial class FirstSeedDataTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "ParkedVehicleId", "Color", "Make", "Model", "NumberOfWheels", "RegistrationNumber", "TimeOfArrival", "VehicleType", "Year" },
                values: new object[] { 1, "White", "Volvo", "V70", 4, "ABC123", new DateTime(2023, 11, 8, 12, 9, 39, 99, DateTimeKind.Local).AddTicks(8056), 2, 2022 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "ParkedVehicleId",
                keyValue: 1);
        }
    }
}
