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
                    Keywords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archetype", x => x.ArchetypeId);
                });

            migrationBuilder.CreateTable(
                name: "Talent",
                columns: table => new
                {
                    TalentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Tier",
                columns: table => new
                {
                    TierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tier", x => x.TierId);
                });

            migrationBuilder.CreateTable(
                name: "Wargear",
                columns: table => new
                {
                    WargearId = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_Wargear", x => x.WargearId);
                });

            migrationBuilder.CreateTable(
                name: "WargearLink",
                columns: table => new
                {
                    WargearLinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    WargearId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WargearLink", x => x.WargearLinkId);
                });

            migrationBuilder.CreateTable(
                name: "ArchetypeAbility",
                columns: table => new
                {
                    ArchetypeAbilityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArchetypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchetypeAbility", x => x.ArchetypeAbilityId);
                    table.ForeignKey(
                        name: "FK_ArchetypeAbility_Archetype_ArchetypeId",
                        column: x => x.ArchetypeId,
                        principalTable: "Archetype",
                        principalColumn: "ArchetypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    XP = table.Column<int>(type: "int", nullable: false),
                    TierId = table.Column<int>(type: "int", nullable: true),
                    ArchetypeId = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Character_Tier_TierId",
                        column: x => x.TierId,
                        principalTable: "Tier",
                        principalColumn: "TierId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "CombatTraits",
                columns: table => new
                {
                    CombatTraitsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    Resilence = table.Column<int>(type: "int", nullable: false),
                    Soak = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    Shock = table.Column<int>(type: "int", nullable: false),
                    Wounds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatTraits", x => x.CombatTraitsId);
                    table.ForeignKey(
                        name: "FK_CombatTraits_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MentalTraits",
                columns: table => new
                {
                    MentalTraitsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    Conviction = table.Column<int>(type: "int", nullable: false),
                    Corruption = table.Column<int>(type: "int", nullable: false),
                    PassiveAwareness = table.Column<int>(type: "int", nullable: false),
                    Resolve = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MentalTraits", x => x.MentalTraitsId);
                    table.ForeignKey(
                        name: "FK_MentalTraits_Character_CharacterId",
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

            migrationBuilder.CreateTable(
                name: "SocialTraits",
                columns: table => new
                {
                    SocialTraitsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    Influence = table.Column<int>(type: "int", nullable: false),
                    Wealth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialTraits", x => x.SocialTraitsId);
                    table.ForeignKey(
                        name: "FK_SocialTraits_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchetypeAbility_ArchetypeId",
                table: "ArchetypeAbility",
                column: "ArchetypeId",
                unique: true);

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
                name: "IX_Character_TierId",
                table: "Character",
                column: "TierId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatTraits_CharacterId",
                table: "CombatTraits",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MentalTraits_CharacterId",
                table: "MentalTraits",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CharacterId",
                table: "Skills",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialTraits_CharacterId",
                table: "SocialTraits",
                column: "CharacterId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchetypeAbility");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "CombatTraits");

            migrationBuilder.DropTable(
                name: "MentalTraits");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "SocialTraits");

            migrationBuilder.DropTable(
                name: "Talent");

            migrationBuilder.DropTable(
                name: "TalentLink");

            migrationBuilder.DropTable(
                name: "Wargear");

            migrationBuilder.DropTable(
                name: "WargearLink");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Archetype");

            migrationBuilder.DropTable(
                name: "Tier");
        }
    }
}
