using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CharacterManager.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Archetype",
                columns: table => new
                {
                    ArchetypeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    XPCost = table.Column<int>(type: "INTEGER", nullable: false),
                    Tier = table.Column<int>(type: "INTEGER", nullable: false),
                    ArchetypeAbility = table.Column<string>(type: "TEXT", nullable: true),
                    AttributeBonus = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillBonus = table.Column<int>(type: "INTEGER", nullable: false),
                    Influence = table.Column<int>(type: "INTEGER", nullable: false),
                    Keywords = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archetype", x => x.ArchetypeId);
                });

            migrationBuilder.CreateTable(
                name: "ArchetypeLink",
                columns: table => new
                {
                    ArchetypeLinkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ArchetypeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchetypeLink", x => x.ArchetypeLinkId);
                });

            migrationBuilder.CreateTable(
                name: "Armor",
                columns: table => new
                {
                    ArmorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    AR = table.Column<int>(type: "INTEGER", nullable: false),
                    Traits = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Keywords = table.Column<string>(type: "TEXT", nullable: true),
                    IsEquipped = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armor", x => x.ArmorId);
                });

            migrationBuilder.CreateTable(
                name: "ArmorLink",
                columns: table => new
                {
                    ArmorLinkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ArmorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmorLink", x => x.ArmorLinkId);
                });

            migrationBuilder.CreateTable(
                name: "Gear",
                columns: table => new
                {
                    GearId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Effect = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Rarity = table.Column<string>(type: "TEXT", nullable: true),
                    Keywords = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gear", x => x.GearId);
                });

            migrationBuilder.CreateTable(
                name: "GearLink",
                columns: table => new
                {
                    GearLinkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GearId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearLink", x => x.GearLinkId);
                });

            migrationBuilder.CreateTable(
                name: "Talent",
                columns: table => new
                {
                    TalentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Requirements = table.Column<string>(type: "TEXT", nullable: true),
                    XPCost = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talent", x => x.TalentId);
                });

            migrationBuilder.CreateTable(
                name: "TalentLink",
                columns: table => new
                {
                    TalentLinkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TalentId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalentLink", x => x.TalentLinkId);
                });

            migrationBuilder.CreateTable(
                name: "Weapon",
                columns: table => new
                {
                    WeaponId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Damage = table.Column<int>(type: "INTEGER", nullable: false),
                    ED = table.Column<int>(type: "INTEGER", nullable: false),
                    AP = table.Column<int>(type: "INTEGER", nullable: false),
                    Salvo = table.Column<string>(type: "TEXT", nullable: true),
                    Range = table.Column<string>(type: "TEXT", nullable: true),
                    Traits = table.Column<string>(type: "TEXT", nullable: true),
                    IsMelee = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsEquipped = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapon", x => x.WeaponId);
                });

            migrationBuilder.CreateTable(
                name: "WeaponLink",
                columns: table => new
                {
                    WeaponLinkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    WeaponId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponLink", x => x.WeaponLinkId);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    XP = table.Column<int>(type: "INTEGER", nullable: false),
                    Tier = table.Column<int>(type: "INTEGER", nullable: false),
                    Rank = table.Column<int>(type: "INTEGER", nullable: false),
                    Wrath = table.Column<int>(type: "INTEGER", nullable: false),
                    Glory = table.Column<int>(type: "INTEGER", nullable: false),
                    ArchetypeId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_Character_Archetype_ArchetypeId",
                        column: x => x.ArchetypeId,
                        principalTable: "Archetype",
                        principalColumn: "ArchetypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Strength = table.Column<int>(type: "INTEGER", nullable: false),
                    Agility = table.Column<int>(type: "INTEGER", nullable: false),
                    Toughness = table.Column<int>(type: "INTEGER", nullable: false),
                    Intellect = table.Column<int>(type: "INTEGER", nullable: false),
                    Willpower = table.Column<int>(type: "INTEGER", nullable: false),
                    Fellowship = table.Column<int>(type: "INTEGER", nullable: false),
                    Initiative = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.AttributesId);
                    table.ForeignKey(
                        name: "FK_Attributes_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Athletics = table.Column<int>(type: "INTEGER", nullable: false),
                    Awareness = table.Column<int>(type: "INTEGER", nullable: false),
                    Ballistic = table.Column<int>(type: "INTEGER", nullable: false),
                    Cunning = table.Column<int>(type: "INTEGER", nullable: false),
                    Deception = table.Column<int>(type: "INTEGER", nullable: false),
                    Insight = table.Column<int>(type: "INTEGER", nullable: false),
                    Intimidation = table.Column<int>(type: "INTEGER", nullable: false),
                    Investigation = table.Column<int>(type: "INTEGER", nullable: false),
                    Leadership = table.Column<int>(type: "INTEGER", nullable: false),
                    Medicae = table.Column<int>(type: "INTEGER", nullable: false),
                    Persuasion = table.Column<int>(type: "INTEGER", nullable: false),
                    Pilot = table.Column<int>(type: "INTEGER", nullable: false),
                    Pyschic = table.Column<int>(type: "INTEGER", nullable: false),
                    Scholar = table.Column<int>(type: "INTEGER", nullable: false),
                    Stealth = table.Column<int>(type: "INTEGER", nullable: false),
                    Survival = table.Column<int>(type: "INTEGER", nullable: false),
                    Tech = table.Column<int>(type: "INTEGER", nullable: false),
                    Weapon = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillsId);
                    table.ForeignKey(
                        name: "FK_Skills_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CharacterId",
                table: "Attributes",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Character_ArchetypeId",
                table: "Character",
                column: "ArchetypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CharacterId",
                table: "Skills",
                column: "CharacterId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchetypeLink");

            migrationBuilder.DropTable(
                name: "Armor");

            migrationBuilder.DropTable(
                name: "ArmorLink");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "Gear");

            migrationBuilder.DropTable(
                name: "GearLink");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Talent");

            migrationBuilder.DropTable(
                name: "TalentLink");

            migrationBuilder.DropTable(
                name: "Weapon");

            migrationBuilder.DropTable(
                name: "WeaponLink");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Archetype");
        }
    }
}
