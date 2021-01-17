﻿// <auto-generated />
using CharacterManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CharacterManager.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("CharacterManager.Models.Archetype", b =>
                {
                    b.Property<int>("ArchetypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ArchetypeAbility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AttributeBonus")
                        .HasColumnType("int");

                    b.Property<int>("Influence")
                        .HasColumnType("int");

                    b.Property<string>("Keywords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SkillBonus")
                        .HasColumnType("int");

                    b.Property<int>("Tier")
                        .HasColumnType("int");

                    b.Property<int>("XPCost")
                        .HasColumnType("int");

                    b.HasKey("ArchetypeId");

                    b.ToTable("Archetype");
                });

            modelBuilder.Entity("CharacterManager.Models.Armor", b =>
                {
                    b.Property<int>("ArmorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AR")
                        .HasColumnType("int");

                    b.Property<string>("Keywords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Traits")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArmorId");

                    b.ToTable("Armor");
                });

            modelBuilder.Entity("CharacterManager.Models.Attributes", b =>
                {
                    b.Property<int>("AttributesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Agility")
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("Fellowship")
                        .HasColumnType("int");

                    b.Property<int>("Initiative")
                        .HasColumnType("int");

                    b.Property<int>("Intellect")
                        .HasColumnType("int");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.Property<int>("Toughness")
                        .HasColumnType("int");

                    b.Property<int>("Willpower")
                        .HasColumnType("int");

                    b.HasKey("AttributesId");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("CharacterManager.Models.Character", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("Tier")
                        .HasColumnType("int");

                    b.Property<int>("XP")
                        .HasColumnType("int");

                    b.HasKey("CharacterId");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.ArchetypeLink", b =>
                {
                    b.Property<int>("ArchetypeLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ArchetypeId")
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.HasKey("ArchetypeLinkId");

                    b.ToTable("ArchetypeLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.ArmorLink", b =>
                {
                    b.Property<int>("ArmorLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ArmorId")
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.HasKey("ArmorLinkId");

                    b.ToTable("ArmorLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.GearLink", b =>
                {
                    b.Property<int>("GearLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("GearId")
                        .HasColumnType("int");

                    b.HasKey("GearLinkId");

                    b.ToTable("GearLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.TalentLink", b =>
                {
                    b.Property<int>("TalentLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("TalentId")
                        .HasColumnType("int");

                    b.HasKey("TalentLinkId");

                    b.ToTable("TalentLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.WeaponLink", b =>
                {
                    b.Property<int>("WeaponLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("WeaponId")
                        .HasColumnType("int");

                    b.HasKey("WeaponLinkId");

                    b.ToTable("WeaponLink");
                });

            modelBuilder.Entity("CharacterManager.Models.Gear", b =>
                {
                    b.Property<int>("GearId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Effect")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Keywords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rarity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("GearId");

                    b.ToTable("Gear");
                });

            modelBuilder.Entity("CharacterManager.Models.Skills", b =>
                {
                    b.Property<int>("SkillsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Athletics")
                        .HasColumnType("int");

                    b.Property<int>("Awareness")
                        .HasColumnType("int");

                    b.Property<int>("Ballistic")
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("Cunning")
                        .HasColumnType("int");

                    b.Property<int>("Deception")
                        .HasColumnType("int");

                    b.Property<int>("Insight")
                        .HasColumnType("int");

                    b.Property<int>("Intimidation")
                        .HasColumnType("int");

                    b.Property<int>("Investigation")
                        .HasColumnType("int");

                    b.Property<int>("Leadership")
                        .HasColumnType("int");

                    b.Property<int>("Medicae")
                        .HasColumnType("int");

                    b.Property<int>("Persuasion")
                        .HasColumnType("int");

                    b.Property<int>("Pilot")
                        .HasColumnType("int");

                    b.Property<int>("Pyschic")
                        .HasColumnType("int");

                    b.Property<int>("Scholar")
                        .HasColumnType("int");

                    b.Property<int>("Stealth")
                        .HasColumnType("int");

                    b.Property<int>("Survival")
                        .HasColumnType("int");

                    b.Property<int>("Tech")
                        .HasColumnType("int");

                    b.Property<int>("Weapon")
                        .HasColumnType("int");

                    b.HasKey("SkillsId");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("CharacterManager.Models.Talent", b =>
                {
                    b.Property<int>("TalentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Requirements")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("XPCost")
                        .HasColumnType("int");

                    b.HasKey("TalentId");

                    b.ToTable("Talent");
                });

            modelBuilder.Entity("CharacterManager.Models.Weapon", b =>
                {
                    b.Property<int>("WeaponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AP")
                        .HasColumnType("int");

                    b.Property<string>("Damage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Range")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salvo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Traits")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WeaponId");

                    b.ToTable("Weapon");
                });

            modelBuilder.Entity("CharacterManager.Models.Attributes", b =>
                {
                    b.HasOne("CharacterManager.Models.Character", null)
                        .WithOne("Attributes")
                        .HasForeignKey("CharacterManager.Models.Attributes", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharacterManager.Models.Skills", b =>
                {
                    b.HasOne("CharacterManager.Models.Character", null)
                        .WithOne("Skills")
                        .HasForeignKey("CharacterManager.Models.Skills", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharacterManager.Models.Character", b =>
                {
                    b.Navigation("Attributes");

                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
