using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrewSchedule.Infrastructure.Migrations.FlightHoursDb
{
    /// <inheritdoc />
    public partial class InitialFlightHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrewFlightHours",
                columns: table => new
                {
                    CrewMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewFlightHours", x => x.CrewMemberId);
                });

            migrationBuilder.CreateTable(
                name: "CrewMember",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewMember", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DepartureAirport = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ArrivalAirport = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepartureTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SwapRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromCrewMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToCrewMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwapRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightTimeEntry",
                columns: table => new
                {
                    CrewFlightHoursId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightTimeEntry", x => new { x.CrewFlightHoursId, x.Id });
                    table.ForeignKey(
                        name: "FK_FlightTimeEntry_CrewFlightHours_CrewFlightHoursId",
                        column: x => x.CrewFlightHoursId,
                        principalTable: "CrewFlightHours",
                        principalColumn: "CrewMemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    FlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CrewMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckInTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOutTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlightId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => new { x.FlightId, x.CrewMemberId });
                    table.ForeignKey(
                        name: "FK_Assignment_CrewMember_CrewMemberId",
                        column: x => x.CrewMemberId,
                        principalTable: "CrewMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignment_Flight_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignment_Flight_FlightId1",
                        column: x => x.FlightId1,
                        principalTable: "Flight",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_CrewMemberId",
                table: "Assignment",
                column: "CrewMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_FlightId1",
                table: "Assignment",
                column: "FlightId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "FlightTimeEntry");

            migrationBuilder.DropTable(
                name: "SwapRequest");

            migrationBuilder.DropTable(
                name: "CrewMember");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "CrewFlightHours");
        }
    }
}
