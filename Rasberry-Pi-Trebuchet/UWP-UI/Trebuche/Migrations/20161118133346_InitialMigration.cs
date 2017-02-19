using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trebuchet.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PiConfigurationSettings",
                columns: table => new
                {
                    PiConfigid = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    LedLightColor = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PiIp = table.Column<string>(nullable: true),
                    PiName = table.Column<string>(nullable: true),
                    SendToast = table.Column<bool>(nullable: false),
                    UserAzure = table.Column<bool>(nullable: false),
                    isConfigurationSetting = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PiConfigurationSettings", x => x.PiConfigid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PiConfigurationSettings");
        }
    }
}
