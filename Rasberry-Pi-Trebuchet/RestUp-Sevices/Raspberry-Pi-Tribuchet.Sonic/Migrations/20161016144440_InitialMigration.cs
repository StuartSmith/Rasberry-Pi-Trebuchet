using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Raspberry_Pi_Trebuchet.RestUp.Sonic.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UltraSonicSensorRuns",
                columns: table => new
                {
                    SonicId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    MeasurementIn = table.Column<string>(nullable: true),
                    PinGPIOEcho = table.Column<int>(nullable: false),
                    PinGPIOTrigger = table.Column<int>(nullable: false),
                    RequestSentToAzure = table.Column<bool>(nullable: false),
                    RunDate = table.Column<DateTime>(nullable: false),
                    SonicGUID = table.Column<string>(nullable: true),
                    TotalDistance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UltraSonicSensorRuns", x => x.SonicId);
                });

            migrationBuilder.CreateTable(
                name: "UltraSonicSensorRunMeasurements",
                columns: table => new
                {
                    SonicMeasurementId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    MeasurementDistance = table.Column<double>(nullable: false),
                    MeasurementGUID = table.Column<string>(nullable: true),
                    SonicGUID = table.Column<string>(nullable: true),
                    TimeOfMeasurment = table.Column<DateTime>(nullable: false),
                    UltraSonicSensorRunId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UltraSonicSensorRunMeasurements", x => x.SonicMeasurementId);
                    table.ForeignKey(
                        name: "FK_UltraSonicSensorRunMeasurements_UltraSonicSensorRuns_UltraSonicSensorRunId",
                        column: x => x.UltraSonicSensorRunId,
                        principalTable: "UltraSonicSensorRuns",
                        principalColumn: "SonicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UltraSonicSensorRunMeasurements_UltraSonicSensorRunId",
                table: "UltraSonicSensorRunMeasurements",
                column: "UltraSonicSensorRunId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UltraSonicSensorRunMeasurements");

            migrationBuilder.DropTable(
                name: "UltraSonicSensorRuns");
        }
    }
}
