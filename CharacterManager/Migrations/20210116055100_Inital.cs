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
                    ArchetypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XPCost = table.Column<int>(type: "int", nullable: false),
                    Tier = table.Column<int>(type: "int", nullable: false),
                    ArchetypeAbility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttributeBonus = table.Column<int>(type: "int", nullable: false),
                    SkillBonus = table.Column<int>(type: "int", nullable: false),
                    Influence = table.Column<int>(type: "int", nullable: false),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archetype", x => x.ArchetypeId);
                });

            migrationBuilder.CreateTable(
                name: "ArchetypeLink",
                columns: table => new
                {
                    ArchetypeLinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArchetypeId = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchetypeLink", x => x.ArchetypeLinkId);
                });

            migrationBuilder.CreateTable(
                name: "Armor",
                columns: table => new
                {
                    ArmorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AR = table.Column<int>(type: "int", nullable: false),
                    Traits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armor", x => x.ArmorId);
                });

            migrationBuilder.CreateTable(
                name: "ArmorLink",
                columns: table => new
                {
                    ArmorLinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArmorId = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArmorLink", x => x.ArmorLinkId);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XP = table.Column<int>(type: "int", nullable: false),
                    Tier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.CharacterId);
                });

            migrationBuilder.CreateTable(
                name: "Gear",
                columns: table => new
                {
                    GearId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gear", x => x.GearId);
                });

            migrationBuilder.CreateTable(
                name: "Talent",
                columns: table => new
                {
                    TalentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XPCost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talent", x => x.TalentId);
                });

            migrationBuilder.CreateTable(
                name: "TalentLink",
                columns: table => new
                {
                    TalentLinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    TalentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalentLink", x => x.TalentLinkId);
                });

            migrationBuilder.CreateTable(
                name: "WargearLink",
                columns: table => new
                {
                    GearLinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    GearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WargearLink", x => x.GearLinkId);
                });

            migrationBuilder.CreateTable(
                name: "Weapon",
                columns: table => new
                {
                    WeaponId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Damage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AP = table.Column<int>(type: "int", nullable: false),
                    Salvo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Range = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Traits = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weapon", x => x.WeaponId);
                });

            migrationBuilder.CreateTable(
                name: "WeaponLink",
                columns: table => new
                {
                    WeaponLinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeaponId = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponLink", x => x.WeaponLinkId);
                });

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    AttributesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Agility = table.Column<int>(type: "int", nullable: false),
                    Toughness = table.Column<int>(type: "int", nullable: false),
                    Intellect = table.Column<int>(type: "int", nullable: false),
                    Willpower = table.Column<int>(type: "int", nullable: false),
                    Fellowship = table.Column<int>(type: "int", nullable: false),
                    Initiative = table.Column<int>(type: "int", nullable: false)
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
                    SkillsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    Athletics = table.Column<int>(type: "int", nullable: false),
                    Awareness = table.Column<int>(type: "int", nullable: false),
                    Ballistic = table.Column<int>(type: "int", nullable: false),
                    Cunning = table.Column<int>(type: "int", nullable: false),
                    Deception = table.Column<int>(type: "int", nullable: false),
                    Insight = table.Column<int>(type: "int", nullable: false),
                    Intimidation = table.Column<int>(type: "int", nullable: false),
                    Investigation = table.Column<int>(type: "int", nullable: false),
                    Leadership = table.Column<int>(type: "int", nullable: false),
                    Medicae = table.Column<int>(type: "int", nullable: false),
                    Persuasion = table.Column<int>(type: "int", nullable: false),
                    Pilot = table.Column<int>(type: "int", nullable: false),
                    Pyschic = table.Column<int>(type: "int", nullable: false),
                    Scholar = table.Column<int>(type: "int", nullable: false),
                    Stealth = table.Column<int>(type: "int", nullable: false),
                    Survival = table.Column<int>(type: "int", nullable: false),
                    Tech = table.Column<int>(type: "int", nullable: false),
                    Weapon = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_Skills_CharacterId",
                table: "Skills",
                column: "CharacterId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archetype");

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
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Talent");

            migrationBuilder.DropTable(
                name: "TalentLink");

            migrationBuilder.DropTable(
                name: "WargearLink");

            migrationBuilder.DropTable(
                name: "Weapon");

            migrationBuilder.DropTable(
                name: "WeaponLink");

            migrationBuilder.DropTable(
                name: "Character");
        }
    }
}
