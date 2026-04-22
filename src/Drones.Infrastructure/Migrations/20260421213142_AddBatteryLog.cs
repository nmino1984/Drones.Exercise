using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drones.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBatteryLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BatteryLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DroneId = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    BatteryLevel = table.Column<double>(type: "float", nullable: false),
                    CheckedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryLog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatteryLog");
        }
    }
}
