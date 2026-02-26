using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrewSchedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "CrewMembers");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "CrewMembers",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "ArrivalAirport", "ArrivalTimeUtc", "DepartureAirport", "DepartureTimeUtc", "FlightNumber" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "SCO", new DateTime(2025, 3, 1, 12, 0, 0, 0, DateTimeKind.Utc), "ALA", new DateTime(2025, 3, 1, 10, 0, 0, 0, DateTimeKind.Utc), "KC123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DropColumn(
                name: "Role",
                table: "CrewMembers");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "CrewMembers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
