﻿// <auto-generated />
using System;
using CharacterManager.DAC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CharacterManager.DAC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210405024552_Inital")]
    partial class Inital
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("CharacterManager.Models.Archetype", b =>
                {
                    b.Property<Guid>("ArchetypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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
                    b.Property<Guid>("ArmorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AR")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEquipped")
                        .HasColumnType("bit");

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
                    b.Property<Guid>("AttributesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Agility")
                        .HasColumnType("int");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

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
                    b.Property<Guid>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ArchetypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Glory")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("Tier")
                        .HasColumnType("int");

                    b.Property<int>("Wrath")
                        .HasColumnType("int");

                    b.Property<int>("XP")
                        .HasColumnType("int");

                    b.HasKey("CharacterId");

                    b.HasIndex("ArchetypeId");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.ArchetypeLink", b =>
                {
                    b.Property<Guid>("ArchetypeLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArchetypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ArchetypeLinkId");

                    b.ToTable("ArchetypeLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.ArmorLink", b =>
                {
                    b.Property<Guid>("ArmorLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArmorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ArmorLinkId");

                    b.ToTable("ArmorLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.GearLink", b =>
                {
                    b.Property<Guid>("GearLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GearId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GearLinkId");

                    b.ToTable("GearLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.TalentLink", b =>
                {
                    b.Property<Guid>("TalentLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TalentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TalentLinkId");

                    b.ToTable("TalentLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.WeaponLink", b =>
                {
                    b.Property<Guid>("WeaponLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("WeaponId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("WeaponLinkId");

                    b.ToTable("WeaponLink");
                });

            modelBuilder.Entity("CharacterManager.Models.ConfigParam", b =>
                {
                    b.Property<int>("ConfigParamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConfigParamId");

                    b.ToTable("ConfigParams");
                });

            modelBuilder.Entity("CharacterManager.Models.Gear", b =>
                {
                    b.Property<Guid>("GearId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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
                    b.Property<Guid>("SkillsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Athletics")
                        .HasColumnType("int");

                    b.Property<int>("Awareness")
                        .HasColumnType("int");

                    b.Property<int>("Ballistic")
                        .HasColumnType("int");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uniqueidentifier");

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
                    b.Property<Guid>("TalentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

            modelBuilder.Entity("CharacterManager.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SourceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SourceMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceRepository")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransactionId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CharacterManager.Models.Weapon", b =>
                {
                    b.Property<Guid>("WeaponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AP")
                        .HasColumnType("int");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ED")
                        .HasColumnType("int");

                    b.Property<bool>("IsEquipped")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMelee")
                        .HasColumnType("bit");

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

            modelBuilder.Entity("CharacterManager.Models.Character", b =>
                {
                    b.HasOne("CharacterManager.Models.Archetype", "Archetype")
                        .WithMany()
                        .HasForeignKey("ArchetypeId");

                    b.Navigation("Archetype");
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
