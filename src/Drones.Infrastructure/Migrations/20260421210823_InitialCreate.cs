using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Drones.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "nState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tMedication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Code = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tMedication", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tDrone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Model = table.Column<int>(type: "int", nullable: false),
                    WeightLimit = table.Column<double>(type: "float", nullable: false),
                    BatteryCapacity = table.Column<double>(type: "float", nullable: false),
                    BatteryLevel = table.Column<double>(type: "float", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    NModelId = table.Column<int>(type: "int", nullable: true),
                    NStateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tDrone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tDrone_nModel_NModelId",
                        column: x => x.NModelId,
                        principalTable: "nModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tDrone_nState_NStateId",
                        column: x => x.NStateId,
                        principalTable: "nState",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "rDroneMedication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDrone = table.Column<int>(type: "int", nullable: false),
                    IdMedication = table.Column<int>(type: "int", nullable: false),
                    DateOpperation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rDroneMedication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rDroneMedication_tDrone",
                        column: x => x.IdDrone,
                        principalTable: "tDrone",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_rDroneMedication_tMedication",
                        column: x => x.IdMedication,
                        principalTable: "tMedication",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_rDroneMedication_IdDrone",
                table: "rDroneMedication",
                column: "IdDrone");

            migrationBuilder.CreateIndex(
                name: "IX_rDroneMedication_IdMedication",
                table: "rDroneMedication",
                column: "IdMedication");

            migrationBuilder.CreateIndex(
                name: "IX_tDrone_NModelId",
                table: "tDrone",
                column: "NModelId");

            migrationBuilder.CreateIndex(
                name: "IX_tDrone_NStateId",
                table: "tDrone",
                column: "NStateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rDroneMedication");

            migrationBuilder.DropTable(
                name: "tDrone");

            migrationBuilder.DropTable(
                name: "tMedication");

            migrationBuilder.DropTable(
                name: "nModel");

            migrationBuilder.DropTable(
                name: "nState");
        }
    }
}
