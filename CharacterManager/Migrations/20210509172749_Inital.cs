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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    table.PrimaryKey("PK_Archetype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Armor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    table.PrimaryKey("PK_Armor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSync",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Json = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSync", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gear",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Effect = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Rarity = table.Column<string>(type: "TEXT", nullable: true),
                    Keywords = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gear", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestSync",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Json = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestSync", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SyncStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsDownSyncStatus = table.Column<bool>(type: "INTEGER", nullable: false),
                    CharacterLastSync = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArchetypeLastSync = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArmorLastSync = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GearLastSync = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TalentLastSync = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WeaponLastSync = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PsychicLastSync = table.Column<DateTime>(type: "TEXT", nullable: false),
                    QuestLastSync = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Talent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Requirements = table.Column<string>(type: "TEXT", nullable: true),
                    XPCost = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SourceRepository = table.Column<string>(type: "TEXT", nullable: true),
                    SourceMethod = table.Column<string>(type: "TEXT", nullable: true),
                    SourceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                });

            migrationBuilder.CreateTable(
                name: "Weapon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    table.PrimaryKey("PK_Weapon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    XP = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentWounds = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentShock = table.Column<int>(type: "INTEGER", nullable: false),
                    Tier = table.Column<int>(type: "INTEGER", nullable: false),
                    Rank = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Wrath = table.Column<int>(type: "INTEGER", nullable: false),
                    Glory = table.Column<int>(type: "INTEGER", nullable: false),
                    AvatarPath = table.Column<string>(type: "TEXT", nullable: true),
                    ArchetypeId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ArmorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Archetype_ArchetypeId",
                        column: x => x.ArchetypeId,
                        principalTable: "Archetype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Character_Armor_ArmorId",
                        column: x => x.ArmorId,
                        principalTable: "Armor",
                        principalColumn: "Id",
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterGear",
                columns: table => new
                {
                    CharacterGearId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CharacterGearId1 = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterGear", x => new { x.CharacterGearId, x.CharacterGearId1 });
                    table.ForeignKey(
                        name: "FK_CharacterGear_Character_CharacterGearId1",
                        column: x => x.CharacterGearId1,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterGear_Gear_CharacterGearId",
                        column: x => x.CharacterGearId,
                        principalTable: "Gear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterTalent",
                columns: table => new
                {
                    CharactersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TalentsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterTalent", x => new { x.CharactersId, x.TalentsId });
                    table.ForeignKey(
                        name: "FK_CharacterTalent_Character_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterTalent_Talent_TalentsId",
                        column: x => x.TalentsId,
                        principalTable: "Talent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterWeapon",
                columns: table => new
                {
                    CharactersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    WeaponsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterWeapon", x => new { x.CharactersId, x.WeaponsId });
                    table.ForeignKey(
                        name: "FK_CharacterWeapon_Character_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterWeapon_Weapon_WeaponsId",
                        column: x => x.WeaponsId,
                        principalTable: "Weapon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsychicPowers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Discipline = table.Column<string>(type: "TEXT", nullable: true),
                    DN = table.Column<int>(type: "INTEGER", nullable: false),
                    Activation = table.Column<string>(type: "TEXT", nullable: true),
                    Duration = table.Column<string>(type: "TEXT", nullable: true),
                    Range = table.Column<string>(type: "TEXT", nullable: true),
                    Effect = table.Column<string>(type: "TEXT", nullable: true),
                    MultiTarget = table.Column<bool>(type: "INTEGER", nullable: false),
                    Keywords = table.Column<string>(type: "TEXT", nullable: true),
                    Potency = table.Column<string>(type: "TEXT", nullable: true),
                    Requirements = table.Column<string>(type: "TEXT", nullable: true),
                    XPCost = table.Column<int>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychicPowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PsychicPowers_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "Id",
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
                name: "IX_Character_ArmorId",
                table: "Character",
                column: "ArmorId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterGear_CharacterGearId1",
                table: "CharacterGear",
                column: "CharacterGearId1");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterTalent_TalentsId",
                table: "CharacterTalent",
                column: "TalentsId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterWeapon_WeaponsId",
                table: "CharacterWeapon",
                column: "WeaponsId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychicPowers_CharacterId",
                table: "PsychicPowers",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CharacterId",
                table: "Skills",
                column: "CharacterId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "CharacterGear");

            migrationBuilder.DropTable(
                name: "CharacterSync");

            migrationBuilder.DropTable(
                name: "CharacterTalent");

            migrationBuilder.DropTable(
                name: "CharacterWeapon");

            migrationBuilder.DropTable(
                name: "PsychicPowers");

            migrationBuilder.DropTable(
                name: "QuestSync");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "SyncStatus");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Gear");

            migrationBuilder.DropTable(
                name: "Talent");

            migrationBuilder.DropTable(
                name: "Weapon");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Archetype");

            migrationBuilder.DropTable(
                name: "Armor");
        }
    }
}
