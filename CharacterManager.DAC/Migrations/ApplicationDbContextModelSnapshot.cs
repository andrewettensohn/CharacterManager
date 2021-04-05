﻿// <auto-generated />
using System;
using CharacterManager.Sync.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CharacterManager.DAC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("CharacterManager.Models.Archetype", b =>
                {
                    b.Property<Guid>("ArchetypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ArchetypeAbility")
                        .HasColumnType("TEXT");

                    b.Property<int>("AttributeBonus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Influence")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Keywords")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("SkillBonus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tier")
                        .HasColumnType("INTEGER");

                    b.Property<int>("XPCost")
                        .HasColumnType("INTEGER");

                    b.HasKey("ArchetypeId");

                    b.ToTable("Archetype");
                });

            modelBuilder.Entity("CharacterManager.Models.Armor", b =>
                {
                    b.Property<Guid>("ArmorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AR")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsEquipped")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Keywords")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Traits")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("ArmorId");

                    b.ToTable("Armor");
                });

            modelBuilder.Entity("CharacterManager.Models.Attributes", b =>
                {
                    b.Property<Guid>("AttributesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Agility")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Fellowship")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Initiative")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Intellect")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Strength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Toughness")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Willpower")
                        .HasColumnType("INTEGER");

                    b.HasKey("AttributesId");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("CharacterManager.Models.Character", b =>
                {
                    b.Property<Guid>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ArchetypeId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Glory")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Rank")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tier")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Wrath")
                        .HasColumnType("INTEGER");

                    b.Property<int>("XP")
                        .HasColumnType("INTEGER");

                    b.HasKey("CharacterId");

                    b.HasIndex("ArchetypeId");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.ArchetypeLink", b =>
                {
                    b.Property<Guid>("ArchetypeLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArchetypeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("TEXT");

                    b.HasKey("ArchetypeLinkId");

                    b.ToTable("ArchetypeLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.ArmorLink", b =>
                {
                    b.Property<Guid>("ArmorLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArmorId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("TEXT");

                    b.HasKey("ArmorLinkId");

                    b.ToTable("ArmorLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.GearLink", b =>
                {
                    b.Property<Guid>("GearLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GearId")
                        .HasColumnType("TEXT");

                    b.HasKey("GearLinkId");

                    b.ToTable("GearLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.TalentLink", b =>
                {
                    b.Property<Guid>("TalentLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TalentId")
                        .HasColumnType("TEXT");

                    b.HasKey("TalentLinkId");

                    b.ToTable("TalentLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.WeaponLink", b =>
                {
                    b.Property<Guid>("WeaponLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("WeaponId")
                        .HasColumnType("TEXT");

                    b.HasKey("WeaponLinkId");

                    b.ToTable("WeaponLink");
                });

            modelBuilder.Entity("CharacterManager.Models.ConfigParam", b =>
                {
                    b.Property<int>("ConfigParamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("ConfigParamId");

                    b.ToTable("ConfigParams");
                });

            modelBuilder.Entity("CharacterManager.Models.Gear", b =>
                {
                    b.Property<Guid>("GearId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Effect")
                        .HasColumnType("TEXT");

                    b.Property<string>("Keywords")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Rarity")
                        .HasColumnType("TEXT");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("GearId");

                    b.ToTable("Gear");
                });

            modelBuilder.Entity("CharacterManager.Models.Skills", b =>
                {
                    b.Property<Guid>("SkillsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Athletics")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Awareness")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ballistic")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Cunning")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Deception")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Insight")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Intimidation")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Investigation")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Leadership")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Medicae")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Persuasion")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Pilot")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Pyschic")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Scholar")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Stealth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Survival")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tech")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Weapon")
                        .HasColumnType("INTEGER");

                    b.HasKey("SkillsId");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("CharacterManager.Models.Talent", b =>
                {
                    b.Property<Guid>("TalentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Requirements")
                        .HasColumnType("TEXT");

                    b.Property<int>("XPCost")
                        .HasColumnType("INTEGER");

                    b.HasKey("TalentId");

                    b.ToTable("Talent");
                });

            modelBuilder.Entity("CharacterManager.Models.Transaction", b =>
                {
                    b.Property<Guid>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SourceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceMethod")
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceRepository")
                        .HasColumnType("TEXT");

                    b.HasKey("TransactionId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CharacterManager.Models.Weapon", b =>
                {
                    b.Property<Guid>("WeaponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("AP")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Damage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("ED")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEquipped")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsMelee")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Range")
                        .HasColumnType("TEXT");

                    b.Property<string>("Salvo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Traits")
                        .HasColumnType("TEXT");

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
