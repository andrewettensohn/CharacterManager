using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CharacterManager.Sync.API.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchetypeModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Json = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchetypeModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArmorModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Json = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmorModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Json = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GearModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Json = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SyncStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CharacterLastSync = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArchetypeLastSync = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArmorLastSync = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GearLastSync = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TalentLastSync = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WeaponLastSync = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TalentModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Json = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalentModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeaponModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Json = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchetypeModels");

            migrationBuilder.DropTable(
                name: "ArmorModels");

            migrationBuilder.DropTable(
                name: "CharacterModels");

            migrationBuilder.DropTable(
                name: "GearModels");

            migrationBuilder.DropTable(
                name: "SyncStatus");

            migrationBuilder.DropTable(
                name: "TalentModels");

            migrationBuilder.DropTable(
                name: "WeaponModels");
        }
    }
}
