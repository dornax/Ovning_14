using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Garage2.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ParkedVehicle",
                columns: new[] { "ParkedVehicleId", "Color", "Make", "Model", "NumberOfWheels", "RegistrationNumber", "TimeOfArrival", "VehicleType", "Year" },
                values: new object[,]
                {
                    { 1, "White", "Volvo", "V70", 4, "ABC123", new DateTime(2023, 11, 8, 13, 13, 3, 681, DateTimeKind.Local).AddTicks(3348), 2, 2022 },
                    { 2, "White", "Saab", "Airbus", 5, "BCD234", new DateTime(2023, 11, 8, 12, 58, 3, 681, DateTimeKind.Local).AddTicks(3355), 0, 2011 },
                    { 3, "Silver", "Boeing", "747", 18, "CDE345", new DateTime(2023, 11, 8, 12, 58, 3, 681, DateTimeKind.Local).AddTicks(3360), 0, 1970 },
                    { 4, "Red", "Yamaha", "AR210", 0, "DEF456", new DateTime(2023, 11, 8, 12, 43, 3, 681, DateTimeKind.Local).AddTicks(3364), 4, 2021 },
                    { 5, "Green", "Mercedes-Benz", "Citaro", 6, "GHI789", new DateTime(2023, 11, 8, 12, 28, 3, 681, DateTimeKind.Local).AddTicks(3369), 3, 2018 },
                    { 6, "Black", "Toyota", "Camry", 4, "JKL012", new DateTime(2023, 11, 8, 12, 13, 3, 681, DateTimeKind.Local).AddTicks(3374), 2, 2019 },
                    { 7, "Blue", "Harley-Davidson", "Iron 883", 2, "MNO345", new DateTime(2023, 11, 8, 11, 58, 3, 681, DateTimeKind.Local).AddTicks(3379), 1, 2020 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "ParkedVehicleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "ParkedVehicleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "ParkedVehicleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "ParkedVehicleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "ParkedVehicleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "ParkedVehicleId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ParkedVehicle",
                keyColumn: "ParkedVehicleId",
                keyValue: 7);
        }
    }
}
